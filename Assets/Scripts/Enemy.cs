using UnityEngine;
using System.Collections;

public class Enemy : AI {

	
	public int pointsForKill;
	public string missionProgressTag = "";
	protected int maxDistanceFromPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	
	void FixedUpdate () {
		Move ();
		if ((transform.position - player.transform.position).magnitude > maxDistanceFromPlayer) {
			Vector3 spawnPos = Spawner.instance.radiusAboutPlayer(Spawner.instance.enemySpawnRadius,1,50);
			Instantiate(Spawner.instance.enemySpawnEffect, spawnPos, Quaternion.identity);
			this.transform.position = spawnPos;
		}
	}
	
	/// <summary>
	/// Health = Health - change
	/// </summary>
	/// <param name="change">Change.</param>
	public void changeHealth(int change) {
		health -= change;
		if (health <= 0) {
			PlayerStates.instance.alterPoints(pointsForKill);
			Spawner.instance.enemyKilled ();
			MissionManager.instance.addProgress(missionProgressTag, 1);
			Destroy (this.gameObject);
		}
	}

}
