using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public static Spawner inst;

	public static Spawner instance {
		get {
			if (inst == null) {
				inst = FindObjectOfType (typeof(Spawner)) as Spawner;
			}
			return inst;
		}
	}

	/// <summary>
	/// Show debug info in scene view
	/// </summary>
	public bool debugMode = true;

	/// <summary>
	/// radius about the player that the enemies can spawn in
	/// </summary>
	public int enemySpawnRadius = 50;

	/// <summary>
	/// maximum amount of enemies possible to have spawned at once
	/// </summary>
	public float maxEnemies = 15;

	/// <summary>
	/// Possible spawn points of the enemies
	/// </summary>
	public List<Transform> possibleEnemySpawns = new List<Transform> ();

	//spawn points closest to the player, calculated when an enemy is spawned
	List<Transform> closeSpawnPoints = new List<Transform> ();

	/// <summary>
	/// Possible enimies that can be spawned
	/// </summary>
	public List<GameObject> enemiesToSpawn = new List<GameObject> ();

	/// <summary>
	/// effect that is instantiated when an enemy is spawned
	/// </summary>
	public GameObject enemySpawnEffect;

	public List<GameObject> collectables = new List<GameObject>();
	public float collSpawnRadius;
	float collScanHeight;
	int currentActiveCollectables;
	public int maxCollectables;
	public float collectableSpawnHeight;
	List<Vector3> localPickupSpawns = new List<Vector3>();

	//current amount of enemies on the scene
	int currentActiveEnemies = 0;

	//player reference.
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		/*when debugging, draws lines between the active spawns and the player.
		//only in scene view
		if (debugMode) {
			setLocalEnemySpawns();
			foreach (Transform t in closeSpawnPoints) {
				Debug.DrawLine (t.position, player.transform.position);
			}
		}*/

		//ensures there is always maxEnemies in the scene.
		while (currentActiveEnemies < maxEnemies) {
			spawnEnemy();
		}
		while (currentActiveCollectables < maxCollectables) {
			spawnCollectable();
		}
	}

	/// <summary>
	/// Spawns a random enemy close to the player.
	/// </summary>
	public void spawnEnemy () {
		//uses localEnemySpawn() to get a coord to spawn the enemy at.
		Vector3 spawn = radiusAboutPlayer (enemySpawnRadius, 1, 50);
		int randomIndexEnemy = Random.Range (0, enemiesToSpawn.Count);
		Instantiate (enemiesToSpawn [randomIndexEnemy], spawn, Quaternion.identity);
		Instantiate (enemySpawnEffect, spawn, Quaternion.identity);
		currentActiveEnemies++;
	}

	/// <summary>
	/// Point near the ground in a radius about the player the about player.
	/// </summary>
	/// <returns>Random point thats near the player.</returns>
	/// <param name="distance">Max distance from player.</param>
	/// <param name="height">Height above ground to place point.</param>
	/// <param name="scanHeight">Height to start scanning down from for raytracing.</param>
	Vector3 radiusAboutPlayer (float distance, float height, float scanHeight) {
		Vector3 tempSpawn = new Vector3(Random.Range (player.transform.position.x-distance,player.transform.position.x+distance),
		                                scanHeight,
		                                Random.Range (player.transform.position.z-distance,player.transform.position.z+distance)
		                                );
		RaycastHit hit;
		if (Physics.Raycast (tempSpawn, Vector3.down, out hit)) {
			tempSpawn = hit.point;
			tempSpawn.y += height;
			return tempSpawn;
		}
		return player.transform.position;
	}

	public void spawnCollectable () {
		Instantiate(collectables[Random.Range (0,collectables.Count-1)], radiusAboutPlayer(collSpawnRadius,0.3f,50), Quaternion.identity);
		currentActiveCollectables++;
	}

	/// <summary>
	/// Should be called whenever an enemy is killed.
	/// ensures that there is aways max enemies in the scene.
	/// </summary>
	public void enemyKilled () {
		spawnEnemy ();
		currentActiveEnemies -= 1;
	}
}