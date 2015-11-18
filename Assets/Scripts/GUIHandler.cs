using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {

	private static GUIHandler inst = null;
	
	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller.
	public static GUIHandler instance {
		get {
			if (inst == null) {
				inst =  FindObjectOfType(typeof (GUIHandler)) as GUIHandler;
			}
			return inst;
		}
	}
	
	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		inst = null;
	}
	
	public Text pointsText;
	public GameObject pointsTextUpdateEffect;
	
	public void updatePointsText (string newText, string updateText) {
		Debug.Log (updateText);
		pointsText.text = "Points: " + newText;
		if (pointsTextUpdateEffect != null) {
			GameObject p = (GameObject)Instantiate (pointsTextUpdateEffect, pointsText.transform.position, Quaternion.identity);
			p.GetComponent<UpscrollingText>().text = updateText;
		}
	}
}
