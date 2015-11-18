using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public int damage;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision c) {
		if (c.transform.tag == "Enemy") {
			c.gameObject.GetComponent<Enemy>().changeHealth(damage);
		}
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}

}
