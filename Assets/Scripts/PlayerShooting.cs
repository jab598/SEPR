using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;

	float lastfireTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time >= lastfireTime + fireRate && PlayerStates.instance.resources != 0) {
			GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
			g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed;
			lastfireTime = Time.time;
			PlayerStates.instance.alterResources(-1);
			GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "-1", true);
		}
	}
}
