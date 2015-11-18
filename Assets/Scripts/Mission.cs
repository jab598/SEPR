using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {

	public enum MissionType {
		Continuous, Discrete
	}

	public bool complete = false;

	public MissionType type;

	public string missionName;

	public int pointsForComplete;

}
