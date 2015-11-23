﻿using UnityEngine;
using System.Collections;

public class EnemyGoose : Enemy {

	/// <summary>
	/// Gameobject that holds the wings
	/// </summary>
	public GameObject wings;

	

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
		if (state == State.Chasing) {
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

}