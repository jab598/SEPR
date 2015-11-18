using UnityEngine;
using System.Collections;

public class Enemy : AI {

	/// <summary>
	/// Gameobject that holds the wings
	/// </summary>
	public GameObject wings;
	
	public int pointsForKill;
	int maxDistanceFromPlayer;
	Spawner spawner;
	PlayerStates playerStates;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
		wings.SetActive (false);
		playerStates = player.GetComponent<PlayerStates> ();
		spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ();
		maxDistanceFromPlayer = spawner.enemySpawnRadius;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			playerStates.alterPoints(pointsForKill);
			spawner.enemyKilled ();
			Destroy (this.gameObject);
		}
		if ((transform.position - playerStates.transform.position).magnitude > maxDistanceFromPlayer) {
			spawner.enemyKilled();
			Destroy (this.gameObject);
		}
		if (state == "chasing") {
			//dont re-order these two methods. We must start the animation before activating the wings
			//otherwise it fails to play.
			anim.Play("chasing");
			wings.SetActive (true);
		} else {
			wings.SetActive (false);
			anim.Play("walking");
		}
	}

	void FixedUpdate () {
		Move ();
	}

	/// <summary>
	/// Health = Health - change
	/// </summary>
	/// <param name="change">Change.</param>
	public void changeHealth(int change) {
		health -= change;
	}

}
