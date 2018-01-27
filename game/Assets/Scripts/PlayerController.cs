using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Control sensitivity")]
	public float movementSpeed; // left right speed
	public float jumpForce; // upward force applied to jump
	public float fallForce; // downward force applied to falling
	//public float speed;

	[Header("Other player settings")]
	public float fallThreshold; // how far the player falls before respawning

	private Rigidbody rb;
	private bool grounded = true; // true iff player is not in the air
	private Vector3 startPosition; // start position of player
	private bool verticalMove = false; // is player vertical movement allowed

	void Start() {
		rb = GetComponent<Rigidbody> ();
		startPosition = transform.position;
	}

	void Update () {
		//speed = rb.velocity.y;
		if (Input.GetAxis ("Horizontal") > 0) {
			transform.position += transform.right * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			transform.position += -transform.right * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Vertical") > 0 && verticalMove) {
			transform.position += transform.forward * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Vertical") < 0 && verticalMove) {
			transform.position += -transform.forward * Time.deltaTime * movementSpeed;
		}
		// toggle vertical movement
		if (Input.GetKeyDown (KeyCode.C)) {
			verticalMove = !verticalMove;
		}
		// jump
		if (grounded && Input.GetKeyDown(KeyCode.Space)) {
			rb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
			grounded = false;
		}
		if(rb.velocity.y < 2){
			rb.AddForce (Vector3.down * fallForce);
		}

		// respawn in start position if player falls off plane
		if (transform.position.y < fallThreshold){
			transform.position = startPosition;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Untagged") {
			grounded = true;
			//Debug.Log ("Grounded again");
		} 
	}

}
