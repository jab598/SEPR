﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpscrollingText : MonoBehaviour {

	public string text;
	public float lifetime;
	public Color textColor = new Color (255, 255, 255, 255);

	public Vector3 movementVector = new Vector3 (0, 1, 0);

	Text textComponent;
	Color faded;

	public UpscrollingText(string text) {
		this.text = text;
	}

	// Use this for initialization
	void Start () {
		this.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform);
		textComponent = this.GetComponent<Text> ();
		textComponent.text = text;
		textComponent.color = textColor;
		faded = textColor;
		faded.a = 0;
		Invoke ("kill", lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (movementVector * Time.deltaTime);
		textComponent.color = Color.Lerp (textComponent.color, faded, 1 / (textComponent.color.a - faded.a) * Time.deltaTime);
	}

	void kill() {
		Destroy (this.gameObject);
	}
}
