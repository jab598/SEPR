using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStates : MonoBehaviour {

	public static PlayerStates inst;

	public static PlayerStates instance {
		get {
			if (inst == null) {
				inst =  FindObjectOfType(typeof (PlayerStates)) as PlayerStates;
			}
			return inst;
		}
	}

	public int points;
	public int resources;
	int geeseKilled = 0;
	int rabbitsKilled = 0;
	int breadCollected = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void alterPoints(int amount) {
		points += amount;
	}

	public void killedGoose () {
		geeseKilled += 1;
		//MissionManager.instance.addProgress (0, 1);
	}


}
