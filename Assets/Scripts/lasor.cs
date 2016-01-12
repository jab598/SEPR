using UnityEngine;
using System.Collections;

public class lasor : MonoBehaviour {

	/// <summary>
	/// The fire rate.
	/// </summary>
	public float fireRate;

	/// <summary>
	/// The damage per tick of fireRate
	/// </summary>
	public int damage;

	//used to track the damage rate of the lasor
	float lastTime;

	void OnTriggerEnter (Collider c) {
		if (c.transform.tag == "Enemy" && Time.time >= lastTime) {
			c.gameObject.GetComponent<Enemy> ().changeHealth (damage);
			lastTime = Time.time;
		}
	}
}