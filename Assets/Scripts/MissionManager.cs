using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

	public static MissionManager inst;
	
	public static MissionManager instance {
		get {
			if (inst == null) {
				inst = FindObjectOfType (typeof(MissionManager)) as MissionManager;
			}
			return inst;
		}
	}

	public List<string> missionTexts = new List<string>();
	public List<int> missionValues = new List<int>();
	public List<int> missionProgress = new List<int> ();
	public List<Text> texts = new List<Text>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateMission(int index, string text, int value, int progress) {
		missionTexts [index] = text;
		missionValues [index] = value;
		missionProgress [index] = progress;
		GUIHandler.instance.updateMissions ();
	}

	public void addProgress(int index, int progress, bool autoUpdateGUI = true) {
		missionProgress [index] += progress;
		if (missionProgress [index] > missionValues [index]) {
			missionProgress[index] = missionValues[index];
		}
		if (autoUpdateGUI) {
			GUIHandler.instance.updateMissions();
		}
	}
}
