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
	public Text energyText;
	public Image energyBar;
	public int maxEnergyWidth;
	public Text missionTextPrefab;
	public GameObject missionPanel;
	
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
	
	public void updatePointsText (string newText, string updateText) {
		Debug.Log (updateText);
		pointsText.text = "Points: " + newText;
		if (pointsTextUpdateEffect != null) {
			GameObject p = (GameObject)Instantiate (pointsTextUpdateEffect, pointsText.transform.position, Quaternion.identity);
			p.GetComponent<UpscrollingText>().text = updateText;
		}
	}

	void buildMissionTexts (int amount) {
		missionTextsPositionOffset = 0;
		foreach(Mission m in MissionManager.instance.missionsDict.Values) {
			Text t = (Text)Instantiate (missionTextPrefab,Vector2.zero, Quaternion.identity);
			t.gameObject.GetComponent<Text>().text = m.missionText + ": " + m.progress.ToString()+"/"+m.completeProgress.ToString();
			t.gameObject.transform.SetParent(missionPanel.transform);
			t.rectTransform.localPosition = new Vector2(-110, 100-20*(1+missionTextsPositionOffset));
			missionTextsPositionOffset++;
		}
	}

	public void updateMissions () {
		for(int i = 0; i < MissionManager.instance.missions.Count; i++) {

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
