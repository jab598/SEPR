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

	public float gameplayLength = 5;

	public List<Mission> missions = new List<Mission>();
	public Dictionary<string, Mission> missionsDict = new Dictionary<string,Mission>();


	// Use this for initialization
	void Awake () {
		foreach (Mission mission in missions) {
			missionsDict[mission.missionTag] = mission;
		}
		gameplayLength *= 60;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addProgress(string tag, int amount, bool updateGUI = true) {
		missionsDict [tag].progress = Mathf.Clamp (missionsDict[tag].progress + amount, 0, missionsDict [tag].completeProgress);
		missionsDict [tag].checkProgress ();
		if (updateGUI) {
			GUIHandler.instance.updateMissions();
		}
	}

	public void replaceMission(string tag, Mission replacement, bool updateGUI = true) {
		missionsDict [tag] = replacement;
		if (updateGUI) {
			GUIHandler.instance.updateMissions();
		}
	}

}
