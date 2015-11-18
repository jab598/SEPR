using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStates : MonoBehaviour {

	int points;
	public Text pointsText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void alterPoints(int amount) {
		points += amount;
		pointsText.text = "Points: " + points.ToString ();
	}
}
