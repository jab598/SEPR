using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {

	public bool complete = false;

	public string missionName;

	public string missionText;

	public string missionTag;

	public int progress;

	public int completeProgress;

	public int pointsForComplete;

	public void checkProgress () {
		if(progress >= completeProgress && complete == false) {
			complete = true;
			PlayerStates.instance.alterPoints(pointsForComplete, true);
		}
	}

}
