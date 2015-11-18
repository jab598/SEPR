using UnityEngine;
using System.Collections;

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
		if (Input.GetButton ("Fire1") && Time.time >= lastfireTime + fireRate) {
			GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
			g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed;
			lastfireTime = Time.time;
		}
	}
}
