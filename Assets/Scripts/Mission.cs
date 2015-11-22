using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {

	public bool complete = false;

	public string missionName;

	public string missionText;

	public string tag;

	public int progress;

	public int completeProgress;

	public int pointsForComplete;

	public void checkProgress () {
		Debug.Log ("Checked");
		if(progress >= completeProgress) {
			complete = true;
		}
	}

}
