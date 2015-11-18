using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float flightSpeed;

	public float lookSensitivity;
	public bool lockCursor;
	public float maximumHeight;
	public float maximumAscentSpeed;
	public float ascentSpeed;
	public Animator duckAnim;
	public GameObject duckWings;

	float currentHeight;

	public Rigidbody r;

	// Use this for initialization
	void Start () {
		r = this.GetComponent<Rigidbody> ();
		if (lockCursor) {
			Screen.lockCursor = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 wantedVel = new Vector3 (0, 0, 0);
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {

			if (distanceToGround () <= 0.25f) {
				wantedVel += transform.forward * Input.GetAxis ("Vertical") * movementSpeed;
				wantedVel += transform.right * Input.GetAxis ("Horizontal") * movementSpeed;
			} else {
				wantedVel += transform.forward * Input.GetAxis ("Vertical") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed);
				wantedVel += transform.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed);
			}
		}
		if (Input.GetAxis ("Mouse X") != 0) {
			transform.RotateAround(transform.position, Vector3.up ,Input.GetAxis("Mouse X") * lookSensitivity);
		}

		if (Input.GetButton ("Jump") && transform.position.y <= maximumHeight) {
			wantedVel.y +=  ascentSpeed;
		}

		if (Input.GetButton ("Decend") && distanceToGround() >= 0) {
			wantedVel.y -= ascentSpeed;
		}

		if (distanceToGround () >= 0.25f) {
			r.useGravity = false;
			duckWings.SetActive(true);;
			duckAnim.Play("Flying");
		} else {
			duckWings.SetActive(false);
			r.useGravity = true;
			duckAnim.Play("Walking");
		}
		r.velocity = wantedVel;
	}

	public float distanceToGround () {		
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			return hit.distance;
		} else {
			return -1;
		}
	}
}
