using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

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
	public float collSpawnDistance;
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
		//when debugging, draws lines between the active spawns and the player.
		//only in scene view
		if (debugMode) {
			setLocalEnemySpawns();
			foreach (Transform t in closeSpawnPoints) {
				Debug.DrawLine (t.position, player.transform.position);
			}
		}

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
		//updates spawns, 
		//setLocalEnemySpawns ();
		//when we have spawns, randomly select a spawn and an enemy, then
		//instantiate the spawn effect prefab and the enemy prefab. add an enemy to
		/*the count
		if (closeSpawnPoints.Count != 0) {
			int randomIndexPos = Random.Range (0, closeSpawnPoints.Count - 1);
			int randomIndexEnemy = Random.Range (0, enemiesToSpawn.Count - 1);
			Instantiate (enemiesToSpawn [randomIndexEnemy], closeSpawnPoints [randomIndexPos].position, closeSpawnPoints [randomIndexPos].rotation);
			Instantiate (enemySpawnEffect, closeSpawnPoints[randomIndexPos].position, closeSpawnPoints[randomIndexPos].rotation);
			currentActiveEnemies++;
		}*/
		Vector3 spawn = localEnemySpawn ();
		int randomIndexEnemy = Random.Range (0, enemiesToSpawn.Count - 1);
		Instantiate (enemiesToSpawn [randomIndexEnemy], spawn, Quaternion.identity);
		Instantiate (enemySpawnEffect, spawn, Quaternion.identity);
		currentActiveEnemies++;
	}

	/// <summary>
	/// Sets closeSpawnPoints to the ones nearest the player.
	/// </summary>
	void setLocalEnemySpawns () {
		closeSpawnPoints = new List<Transform>(possibleEnemySpawns);
		//a "for" loop that handles items being deleted half way through.
		//When an item is deleted, the index isnt incremented.
		int i = 0;
		while (i < closeSpawnPoints.Count) {
			if((closeSpawnPoints[i].position - player.transform.position).magnitude > enemySpawnRadius) {
				closeSpawnPoints.RemoveAt (i);
			} else {
				i++;
			}
		}
	}

	Vector3 localEnemySpawn () {
		Vector3 tempSpawn = new Vector3(Random.Range (player.transform.position.x-collSpawnDistance,player.transform.position.x+collSpawnDistance),
		                                collScanHeight,
		                                Random.Range (player.transform.position.z-collSpawnDistance,player.transform.position.z+collSpawnDistance)
		                                );
		RaycastHit hit;
		if (Physics.Raycast (tempSpawn, Vector3.down, out hit)) {
			tempSpawn = hit.point;
			tempSpawn.y += collectableSpawnHeight;
			return tempSpawn;
		}
		return player.transform.position;
	}

	public void spawnCollectable () {
		Instantiate(collectables[Random.Range (0,collectables.Count-1)], localCollectableSpawn(), Quaternion.identity);
		currentActiveCollectables++;
	}

	Vector3 localCollectableSpawn () {
		Vector3 tempSpawn = new Vector3(Random.Range (player.transform.position.x-collSpawnDistance,player.transform.position.x+collSpawnDistance),
		                                collScanHeight,
		                                Random.Range (player.transform.position.z-collSpawnDistance,player.transform.position.z+collSpawnDistance)
		                                );
		RaycastHit hit;
		if (Physics.Raycast (tempSpawn, Vector3.down, out hit)) {
			tempSpawn = hit.point;
			tempSpawn.y += collectableSpawnHeight;
			return tempSpawn;
		}
		return player.transform.position;
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