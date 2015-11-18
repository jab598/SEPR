﻿using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	/// <summary>
	/// Speed at which the entitiy moves when not near a player
	/// </summary>
	public float patrolSpeed;

	//rotation that the goose wants to move to
	protected Vector3 wantedRot;

	/// <summary>
	/// Distance from the player the entity is until it chases
	/// </summary>
	public int distanceToChasePlayer;

	/// <summary>
	/// Speed at which the entity moves when chasing the player
	/// </summary>
	public int chasingPlayerMoveSpeed;

	/// <summary>
	/// The entitys animator. This should be attached to the same object as this script
	/// </summary>
	public Animator anim;

	//reference to the player objetc, assigned in the sub-class constructor.
	protected GameObject player;

	//reference to the attached rigidbody. Assigned in the sub class constructor.
	protected Rigidbody rigid;

	//state variable to be read by sub class.
	protected string state = "";

	/// <summary>
	/// Current health of the entity
	/// </summary>
	public int health;
	
	float distanceToPlayer () {
		return Mathf.Abs((player.transform.position - this.transform.position).magnitude);
	}


	protected void Move () {
		if (distanceToPlayer () >= distanceToChasePlayer) {
			rigid.MovePosition (transform.position + (transform.forward * patrolSpeed * Time.deltaTime));
			transform.Rotate ((wantedRot - transform.rotation.eulerAngles) * Time.deltaTime * 0.1f);
			if (Mathf.Abs (wantedRot.y - transform.rotation.y) <= 5) {
				wantedRot.y = Random.Range (0, 360);
			}
			RaycastHit r;
			if (Physics.Raycast (transform.position, transform.forward, out r)) {
				if (r.distance <= 2) {
					transform.Rotate (new Vector3 (0, 180, 0));
				}
			}
			state = "walking";
		} else {
			Vector3 lpos = player.transform.position;
			lpos.y = transform.position.y;
			transform.LookAt(lpos);
			rigid.MovePosition(transform.position + (transform.forward * Time.deltaTime * chasingPlayerMoveSpeed));
			state = "chasing";
		}
	}
}