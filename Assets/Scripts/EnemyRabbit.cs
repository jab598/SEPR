﻿using UnityEngine;
using System.Collections;

public class EnemyRabbit : Enemy {

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
		maxDistanceFromPlayer = Spawner.instance.enemySpawnRadius;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Chasing) {
			anim.Play ("chasing");
		} else {
			anim.Play("walking");
		}
	}
}
