using UnityEngine;
using System.Collections;

public class Enemy : AI {

	/// <summary>
	/// Gameobject that holds the wings
	/// </summary>
	public GameObject wings;
	
	public int pointsForKill;
	int maxDistanceFromPlayer;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
		if (wings != null) {
			wings.SetActive (false);
		}
		maxDistanceFromPlayer = Spawner.instance.enemySpawnRadius;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			PlayerStates.instance.alterPoints(pointsForKill);
			GUIHandler.instance.updatePointsText (PlayerStates.instance.points.ToString(), "+"+pointsForKill.ToString());
			Spawner.instance.enemyKilled ();
			Destroy (this.gameObject);
		}
		if ((transform.position - player.transform.position).magnitude > maxDistanceFromPlayer) {
			Spawner.instance.enemyKilled();
			Destroy (this.gameObject);
		}
		if (state == "chasing") {
			//dont re-order these two methods. We must start the animation before activating the wings
			//otherwise it fails to play.
			anim.Play("chasing");
			if(wings != null) {
				wings.SetActive (true);
			}
		} else {
			anim.Play("walking");
			if(wings != null) {
				wings.SetActive (false);
			}
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
