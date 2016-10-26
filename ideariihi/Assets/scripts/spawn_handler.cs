using UnityEngine;
using System.Collections.Generic;

public class spawn_handler : MonoBehaviour {

    public bool spawnEnemies = true;
	public float timeBetweenEnemySpawns = 5f;

	[Header("Player default resistances")]
	[Range(0f, 100f)] public float defaultPlayerFireResistance = 5f;
	[Range(0f, 100f)] public float defaultPlayerIceResistance = 5f;
	[Range(0f, 100f)] public float defaultPlayerLightningResistance = 5f;
	[Range(0f, 100f)] public float defaultPlayerArmor = 5f;

    private GameObject_handler goHandler;
	private guiManager GuiManager;
	public GameObject crosshairGo;

    void Awake()
    {
        goHandler = GetComponent<GameObject_handler>();
		GuiManager = GetComponent<guiManager>();
    }

	public void StartGame() {

		GuiManager.disablMainMenuGo();
		GuiManager.enableGuiCursor();

		crosshairGo.GetComponent<crosshair>().enableCrosshair();

		SpawnPlayer();
		if (spawnEnemies)
			InvokeRepeating("SpawnEnemy", 1f, timeBetweenEnemySpawns);
	}

    private void SpawnPlayer()
    {
		SpawnParticles(Vector3.zero);
		GameObject playerGo = (GameObject)Instantiate(goHandler.playerPrefab, Vector3.zero, Quaternion.identity);
        goHandler.setPlayer(playerGo);

		// set starting resistances.
		playerGo.GetComponent<resistances>().setAllResistances(defaultPlayerIceResistance, defaultPlayerLightningResistance, defaultPlayerFireResistance, defaultPlayerArmor);
    }

    private void SpawnEnemy()
    {
        Vector3 instPos = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), goHandler.enemyPrefab.transform.position.z);
		SpawnParticles(instPos);
		GameObject enemyGo = (GameObject)Instantiate(goHandler.enemyPrefab, instPos, Quaternion.identity);
	}

	private void SpawnParticles(Vector3 pos) {
		GameObject particleInst = (GameObject)Instantiate(goHandler.spawnAreaEffect, pos, Quaternion.identity);
		float duration = particleInst.GetComponent<ParticleSystem>().duration;
		Destroy(particleInst.gameObject, duration);
	}

}
