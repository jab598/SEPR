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

	public enum State {
		Flying, Walking, Swimming
	}

	public State currentState;

	public int points;
	public int resources;

	public int energy = 50;
	public float energyIncreaseRate;
	public float energyFlyingDecreaseRate;
	float lastIncreaseTime;

	int geeseKilled = 0;
	int rabbitsKilled = 0;
	int breadCollected = 0;


	// Use this for initialization
	void Start () {
		lastIncreaseTime = energyIncreaseRate;
		GUIHandler.instance.updateEnergyBar (energy);
	
	}
	
	// Update is called once per frame
	void Update () {
		gradualEnergyIncrease ();
	}

	public void alterPoints(int amount, bool mission = false, bool updateGUI = true) {
		points += amount;
		if (updateGUI) {
			if(!mission) {
				GUIHandler.instance.updatePointsText (points.ToString (), "+" + amount.ToString ());
			} else {
				GUIHandler.instance.updatePointsText (points.ToString(), "+" + amount.ToString(), true);
			}
		}
	}

	public void alterEnergy (int amount) {
		energy = Mathf.Clamp (energy + amount, 0, 100);
	}

	public void setState(State st) {
		currentState = st;
		lastIncreaseTime = Time.time;
	}

	void gradualEnergyIncrease () {
		if (currentState != State.Flying) {
			if (Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyIncreaseRate;
				alterEnergy (1);
				GUIHandler.instance.updateEnergyBar (energy);
			}
		} else {
			if(Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyFlyingDecreaseRate;
				alterEnergy(-1);
				GUIHandler.instance.updateEnergyBar(energy);
			}
		}
	}


}
