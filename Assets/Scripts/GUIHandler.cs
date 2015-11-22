using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
	public Text energyText;
	public Image energyBar;
	public int maxEnergyWidth;
	public Text missionTextPrefab;
	public GameObject missionPanel;

	List<Text> missionTexts = new List<Text> ();
	
	private int missionTextsPositionOffset;

	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		inst = null;
	}

	void Start () {
		buildMissionTexts (MissionManager.instance.missions.Count);
		updateMissions ();
	}

	void Update () {

	}
	
	public void updatePointsText (string newText, string updateText, bool mission = false) {
		Debug.Log (updateText);
		pointsText.text = "Points: " + newText;
		if (pointsTextUpdateEffect != null && !mission) {
			GameObject p = (GameObject)Instantiate (pointsTextUpdateEffect, pointsText.transform.position, Quaternion.identity);
			p.GetComponent<UpscrollingText> ().text = updateText;
		} else if (pointsTextUpdateEffect != null && mission) {
			GameObject p = (GameObject)Instantiate (pointsTextUpdateEffect, pointsText.transform.position, Quaternion.identity);
			p.GetComponent<UpscrollingText> ().text = updateText;
			p.GetComponent<UpscrollingText>().movementVector = new Vector3(p.GetComponent<UpscrollingText>().movementVector.y,0,0);
		}
	}

	void buildMissionTexts (int amount) {
		missionTextsPositionOffset = 0;
		foreach(Mission m in MissionManager.instance.missionsDict.Values) {
			Text t = (Text)Instantiate (missionTextPrefab,Vector2.zero, Quaternion.identity);
			t.gameObject.GetComponent<Text>().text = m.missionText + ": " + m.progress.ToString()+"/"+m.completeProgress.ToString();
			t.gameObject.transform.SetParent(missionPanel.transform);
			t.rectTransform.localPosition = new Vector2(-110, 100-20*(1+missionTextsPositionOffset));
			missionTexts.Add(t);
			missionTextsPositionOffset++;
		}
	}

	public void updateMissions () {
		missionTextsPositionOffset = 0;
		foreach (Mission m in MissionManager.instance.missionsDict.Values) {
			missionTexts[missionTextsPositionOffset].text = m.missionText + ": " + m.progress.ToString() + "/" + m.completeProgress.ToString();
			missionTextsPositionOffset++;
		}
	}

	public void updateEnergyBar (int value) {
		int clampVal = Mathf.Clamp (value, 0, 100);
		energyText.text = "Energy: " + clampVal.ToString() + "%";
		Vector3 newSize = energyBar.rectTransform.sizeDelta;
		newSize.x = clampVal*maxEnergyWidth/100;
		energyBar.rectTransform.sizeDelta = newSize;
	}
	
}
