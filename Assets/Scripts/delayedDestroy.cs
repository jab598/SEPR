using UnityEngine;
using System.Collections;

public class delayedDestroy : MonoBehaviour {

	public float destroyDelay;

	// Use this for initialization
	void Start () {
		Invoke ("k", destroyDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void k() {
		Destroy (this.gameObject);
	}
}
