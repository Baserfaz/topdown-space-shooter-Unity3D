using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class health : MonoBehaviour {

    public float maxHealth = 100f;
    public float maxShield = 0f;

	private Color FadeOutColor = Color.clear;
	private bool destroyOnDeath = false;

    private float currentHealth;
    private float currentShield;
    private bool isDead = false;
    private bool isPlayer = false;
    private bool coroutineStarted = false;

    private GameObject GameMasterGo;
    private guiManager GuiManager;
	private GameObject_handler GoHandler;
	private resistances myResistances;

	private GameObject GuiCanvasGo;
	private GameObject floatingTextPrefab;

    void Awake()
    {
        GameMasterGo = GameObject.Find("GameMaster");
        GuiManager = GameMasterGo.GetComponent<guiManager>();
		GoHandler = GameMasterGo.GetComponent<GameObject_handler>();
		GuiCanvasGo = GuiManager.GuiCanvasGo;
		floatingTextPrefab = GuiManager.guiFloatingDamageTextPrefab;
        if (gameObject.layer == 8) isPlayer = true;

		myResistances = GetComponent<resistances>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        if(isPlayer)
            GuiManager.UpdateHealthText(currentHealth, maxHealth);
    }

	void Update () 
    {
        if (isDead)
        {
            if (coroutineStarted == false)
            {
                GetComponent<Collider2D>().enabled = false;
                coroutineStarted = true;

				StartCoroutine("GraduallyGreyOut");
            }
        }
	}

	private void instantiateParticleEffect() {
		GameObject particleInst = (GameObject)Instantiate(GoHandler.explosionEffect, transform.position, Quaternion.identity);
		Destroy(particleInst.gameObject, particleInst.GetComponent<ParticleSystem>().duration);
	}

	private void instantiateFloatingText(float dmg) {
		GameObject textInst = (GameObject)Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
		textInst.GetComponent<Text>().text = dmg.ToString();
		textInst.transform.SetParent(GuiCanvasGo.transform);
		textInst.transform.position = Camera.main.WorldToScreenPoint(transform.position);
		StartCoroutine(FadeText(textInst));
	}

    public bool getIsDead()
    {
        return isDead;
    }

	public void takeDamage(float dmg, attributes.DamageType type)
    {

		float calculatedDamage = 0f;

		// calculate the total damage.
		switch(type) {
		case attributes.DamageType.Normal:
			calculatedDamage = -(myResistances.getArmor() * 0.01f - 1) * dmg;
			break;
		case attributes.DamageType.Fire:
			calculatedDamage = -(myResistances.getFireResistance() * 0.01f - 1) * dmg;
			break;
		case attributes.DamageType.Ice:
			calculatedDamage = -(myResistances.getIceResistance() * 0.01f - 1) * dmg;
			break;
		case attributes.DamageType.Lightning:
			calculatedDamage = -(myResistances.getLightningResistance() * 0.01f - 1) * dmg;
			break;
		}

		// do the damage.
		currentHealth -= calculatedDamage;

        if (currentHealth < 0f)
        {
            isDead = true;
            currentHealth = 0f;
        }

        if (isPlayer) GuiManager.UpdateHealthText(currentHealth, maxHealth);
		instantiateFloatingText(calculatedDamage);
    }

    public void healDamage(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (isPlayer) GuiManager.UpdateHealthText(currentHealth, maxHealth);
    }

    private IEnumerator GraduallyGreyOut()
    {

        float maxTime = 1f;
        float currentTime = 0f;

        SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();

		Color startColor = mySpriteRenderer.color;

        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime;

			mySpriteRenderer.color = Color.Lerp(startColor, FadeOutColor, currentTime / maxTime);

            yield return null;
        }

		if(destroyOnDeath) {
			Destroy(gameObject);
		} else {
        	mySpriteRenderer.color = FadeOutColor;
			this.enabled = false;
		}
    }

	private IEnumerator FadeText(GameObject textInst) {

		float maxTime = 2f;
		float currentTime = 0f;

		Vector3 targetPosition = new Vector3(0f, 2f, 0f);

		Text myTextInst = textInst.GetComponent<Text>();

		Color startColor = myTextInst.color;
		Vector3 startPos = myTextInst.transform.position;

		while(currentTime < maxTime) {

			currentTime += Time.deltaTime;

			float time = currentTime / maxTime;

			myTextInst.color = Color.Lerp(startColor, Color.clear, time);
			myTextInst.transform.position = Vector3.Lerp(startPos, myTextInst.transform.position + targetPosition, time);

			yield return null;
		}

		Destroy(textInst.gameObject);
	}
}
