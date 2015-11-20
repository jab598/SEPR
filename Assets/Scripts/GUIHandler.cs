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

	
	public Text pointsText;
	public GameObject pointsTextUpdateEffect;
	public Text energy;
	public Image energyBar;
	public int maxEnergyWidth;
	int i;
	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		inst = null;
	}

	void Start () {
		updateMissions ();
		InvokeRepeating ("test", 0, 1);
	}

	void Update () {

	}
	
	public void updatePointsText (string newText, string updateText) {
		Debug.Log (updateText);
		pointsText.text = "Points: " + newText;
		if (pointsTextUpdateEffect != null) {
			GameObject p = (GameObject)Instantiate (pointsTextUpdateEffect, pointsText.transform.position, Quaternion.identity);
			p.GetComponent<UpscrollingText>().text = updateText;
		}
	}

	public void updateMissions () {
		for(int i = 0; i < MissionManager.instance.missionTexts.Count; i++) {
			MissionManager.instance.texts[i].text = MissionManager.instance.missionTexts[i] +": "+MissionManager.instance.missionProgress[i].ToString()+"/"+MissionManager.instance.missionValues[i].ToString();
		}
	}

	public void updateEnergy (int value) {
		int clampVal = Mathf.Clamp (value, 0, 100);
		energy.text = "Energy: " + clampVal.ToString() + "%";
		Vector3 newSize = energyBar.rectTransform.sizeDelta;
		newSize.x = clampVal*maxEnergyWidth/100;
		energyBar.rectTransform.sizeDelta = newSize;
	}

	void test () {
		updateEnergy (i);
		i += 5;
	}
}
