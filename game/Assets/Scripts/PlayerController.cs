using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("Control sensitivity")]
	public bool glidingEnabled; // enables gliding if true
	public float movementSpeed;
	public float jumpForce; // upward force applied to jump
	public float fallForce; // downward force applied to falling
	public float lowJumpForce; // jump adjustment force
	public float speed;
	[Header("Other player settings")]
	public float fallThreshold; // how far the player falls before respawning
	private Vector3 startGravity;
	private Rigidbody rb; // reference to rigidbody of this player
	private bool grounded = true; // true iff player is not in the air
	private Vector3 startPosition; // start position of player
	private int coinCount; // count the coins collected
	public Text countText;
	WorldManager worldManager; // reference to WorldManager

	void Start() {
		worldManager = WorldManager.instance; // set the reference to WorldManager instance
		rb = GetComponent<Rigidbody> (); // get the rigidbody of this player object
		startPosition = transform.position; // store the start position of the player
		startGravity = Physics.gravity;
		coinCount = 0;
		SetCountText();
	}

	void Update () {
		// jump
		if (grounded && Input.GetButtonDown("Jump")) {
			rb.velocity = Vector3.up * jumpForce;
			grounded = false;
		}

		if (rb.velocity.y > 0.1 || rb.velocity.y < -0.1) {
			grounded = false;
		}

		if (glidingEnabled && rb.velocity.y < 0 && Input.GetButton ("Jump")) {
			Physics.gravity = new Vector3(Physics.gravity.x, 0.5F * Physics.gravity.y, Physics.gravity.z);
		}

		if (glidingEnabled && Input.GetButtonUp ("Jump")) {
			Physics.gravity = startGravity;
		}

		// fall faster after jumping up
		if (rb.velocity.y <= 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * fallForce * Time.deltaTime;
		} 
//		else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
		else if (rb.velocity.y > 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * lowJumpForce * Time.deltaTime;
		}

		// respawn in start position if player falls off plane
		if (transform.position.y < fallThreshold){
			transform.position = startPosition;
		}
	}
	void FixedUpdate() {
		speed = rb.velocity.y;
		// player movement
		if (Input.GetAxis ("Horizontal") > 0) {
			transform.position += transform.right * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			transform.position += -transform.right * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Vertical") > 0 && !worldManager.mode2d) {
			transform.position += transform.forward * Time.deltaTime * movementSpeed;
		}
		if (Input.GetAxis ("Vertical") < 0 && !worldManager.mode2d) {
			transform.position += -transform.forward * Time.deltaTime * movementSpeed;
		}
	}

	// return true if player is allowed to jump again
	// (helps prevent infinite jumping)
	void OnCollisionStay(Collision col){
		Debug.Log (rb.velocity.y);
		Debug.Log (col.transform.tag);
//		if (col.transform.tag == "MeshDiff" || col.transform.tag == "ColliderDiff" || col.transform.tag == "Ground" || col.transform.tag == "2D" || col.transform.tag == "3D") {
		if ((rb.velocity.y < 0.1 && rb.velocity.y > -0.1) && (col.transform.tag == "MeshDiff" || col.transform.tag == "ColliderDiff" || col.transform.tag == "Ground" || col.transform.tag == "2D" || col.transform.tag == "3D")) {
			grounded = true;
			// Debug.Log ("Grounded again");
		} 
	}

	// collecting the coin objects
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Coin")){
			other.gameObject.SetActive(false);
			coinCount += 1;
			SetCountText();
		}
	}

	void SetCountText(){
		countText.text = "Coins collected: " + coinCount.ToString();
	}

}
