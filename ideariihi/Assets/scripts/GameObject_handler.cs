using UnityEngine;
using System.Collections;

public class GameObject_handler : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

	[Header("Particle effects")]
    public GameObject JetParticleEffect;
	public GameObject spawnAreaEffect;
	public GameObject explosionEffect;

    private GameObject playerInstance;

    public GameObject getPlayer()
    {
        return playerInstance;
    }

    public void setPlayer(GameObject p)
    {
        playerInstance = p;
    }


}
