using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum State
	{
		Flying, Walking, Swimming
	}

	public State state;

	public float movementSpeed;
	public float flightSpeed;
	public float swimSpeed;

	public float lookSensitivity;
	public bool lockCursor;
	public float maximumHeight;
	public float maximumAscentSpeed;
	public float ascentSpeed;
	public Animator duckAnim;
	public GameObject duckWings;
	public float flightHeight;

	float currentHeight;

	public Rigidbody r;

	bool flying;

	// Use this for initialization
	void Start () {
		r = this.GetComponent<Rigidbody> ();
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
		Vector3 pos = transform.position;
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {

			if (state == State.Walking) {
				pos += transform.forward * Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime;
			} else if(state == State.Flying) {
				pos += transform.forward * Input.GetAxis ("Vertical") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * Mathf.Clamp (distanceToGround ()*2, movementSpeed, flightSpeed) * Time.deltaTime;
			} else {
				pos += transform.forward * Input.GetAxis ("Vertical") * swimSpeed * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * swimSpeed * Time.deltaTime;
			}
		}
		if (Input.GetAxis ("Mouse X") != 0) {
			transform.RotateAround(transform.position, Vector3.up ,Input.GetAxis("Mouse X") * lookSensitivity);
		}

		if (Input.GetButton ("Jump") && transform.position.y <= maximumHeight) {
			pos.y +=  ascentSpeed * Time.deltaTime;
			state = State.Flying;
		}

		if (distanceToGround () <= flightHeight && state != State.Swimming) {
			state = State.Walking;
		}

		if (Input.GetButton ("Decend") && distanceToGround() >= 0) {
			pos.y -= ascentSpeed * Time.deltaTime;
		}

		if (state == State.Flying) {
			r.velocity = new Vector3(0,0,0);
			r.useGravity = false;
			duckWings.SetActive(true);;
			duckAnim.Play("Flying");
			if(distanceToGround() <= flightHeight) {
				state = State.Walking;
			}
		} else {
			duckWings.SetActive(false);
			r.useGravity = true;
			duckAnim.Play("Walking");
		}
		r.MovePosition (pos);
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
