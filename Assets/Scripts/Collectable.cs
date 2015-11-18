using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	/// <summary>
	/// points the player gains for collecting
	/// </summary>
	public int pointsOnCollect;

	/// <summary>
	/// Gameobject that is instantiated when the player collides
	/// </summary>
	public GameObject pointsEffect;

	/// <summary>
	/// Reference to the player states script, for updating points
	/// </summary>
	public PlayerStates statesObj;

	/// <summary>
	/// Does the object bob?
	/// </summary>
	public bool bob;

	/// <summary>
	/// Amount that thet object bobs by (top to bottom): units
	/// </summary>
	public float bobAmount;

	/// <summary>
	/// Speed at which the object bobs: units/second
	/// </summary>
	public float bobSpeed;

	Spawner spawner;

	Transform pointsText;

	Vector3 startPosition;

	GameObject player;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		statesObj = player.GetComponent<PlayerStates> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if we are bobbing, it rotates and bobs the object by the desired amount.
		if (bob) {
			Vector3 wantedPosition = startPosition;
			wantedPosition.y += Mathf.Sin (Time.time * bobSpeed) * bobAmount;
			transform.position = wantedPosition;
			transform.Rotate(new Vector3(0,20*Time.deltaTime,0));
		}
	}

	void LateUpdate () {
		if((this.transform.position - player.transform.position).magnitude > 50) {
			spawner.spawnCollectable();
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider c) {
		if(c.transform.tag == "Player") {
			PlayerStates.instance.alterPoints(pointsOnCollect);
			GUIHandler.instance.updatePointsText(PlayerStates.instance.points.ToString(), "+"+pointsOnCollect.ToString());
			spawner.spawnCollectable();
			Destroy(this.gameObject);
		}
	}
}
