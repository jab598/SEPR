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
			Spawner.instance.enemyKilled();
			Destroy (this.gameObject);
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
