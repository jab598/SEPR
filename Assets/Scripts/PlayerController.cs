using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

	PlayerStates p;

	// Use this for initialization
	void Start () {
		r = this.GetComponent<Rigidbody> ();
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		p = PlayerStates.instance;
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
		Vector3 pos = transform.position;
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			if (p.currentState == PlayerStates.State.Walking) {
				pos += transform.forward * Input.GetAxis ("Vertical") * movementSpeed * Time.deltaTime;
				pos += transform.right * Input.GetAxis ("Horizontal") * movementSpeed * Time.deltaTime;
			} else if(p.currentState == PlayerStates.State.Flying) {
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

		if (Input.GetButton ("Jump") && transform.position.y <= maximumHeight && p.energy >= 0 && p.currentState != PlayerStates.State.Falling) {
			startFlying();
			pos.y +=  ascentSpeed * Time.deltaTime;
		}

		if (Input.GetButton ("Decend") && p.currentState == PlayerStates.State.Flying) {
			pos.y -= ascentSpeed * Time.deltaTime;
		}

		if (distanceToGround () <= flightHeight && p.currentState == PlayerStates.State.Flying) {
			startWalking();
		}

		if (p.currentState == PlayerStates.State.Flying) {
			flying ();
		}

		if (p.currentState == PlayerStates.State.Falling) {
			flying ();
		}

		if (p.currentState == PlayerStates.State.Walking) {
			walking ();
		}

		if (p.currentState == PlayerStates.State.Swimming) {
			swimming ();
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

	public void startFlying () {
		r.velocity = new Vector3(0,0,0);
		r.useGravity = false;
		duckWings.SetActive(true);;
		duckAnim.Play("Flying");
		p.currentState = PlayerStates.State.Flying;
	}

	public void flying () {
		if(distanceToGround() <= flightHeight) {
			p.currentState = PlayerStates.State.Walking;
		}
		if (p.energy == 0) {
			startFalling();
		}
	}

	public void startFalling () {
		duckWings.SetActive (false);
		r.useGravity = true;
		duckAnim.Play ("Walking");
		p.setState(PlayerStates.State.Falling);
	}

	public void startWalking () {
		duckWings.SetActive(false);
		r.useGravity = true;
		duckAnim.Play("Walking");
		p.currentState = PlayerStates.State.Walking;
	}

	public void walking () {
		if (transform.position.y <= 0) {
			startSwimming();
		}

	}

	public void startSwimming () {
		p.currentState = PlayerStates.State.Swimming;
		duckWings.SetActive (false);
		duckAnim.Play ("Walking");
	}

	public void swimming () {
		if (transform.position.y >= 0 && transform.position.y <= flightHeight) {
			startWalking();
		}
	}


}
