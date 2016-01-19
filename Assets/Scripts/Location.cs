﻿using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

	public string[] missionTag;
	bool visited = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player") {
			if(!visited) {
				MissionManager.instance.addProgress(missionTag, 1);
				visited = true;
			}
		}
	}
}
