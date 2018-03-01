using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public float hopHeight = 0.2f;

	private Rigidbody rb; // reference to rigidbody of this player
	private bool grounded = true; // true iff player is not in the air
	private Vector3 startPosition; // start position of player
	public static int coinCount; // count the coins collected
	public Text countText;
	WorldManager worldManager; // reference to WorldManager
	private int boxEncounter = 0;
	Animator player_anim;
	private bool left, right; // animation direction

	void Start() {
		worldManager = WorldManager.instance; // set the reference to WorldManager instance
		rb = GetComponent<Rigidbody> (); // get the rigidbody of this player object
		startPosition = transform.position; // store the start position of the player
		startGravity = Physics.gravity;
		coinCount = 0;
		SetCountText();
		player_anim = GetComponent<Animator>(); //get animator for animation
		right = true;
	}

	void Update () {
		// jump
		if (grounded && Input.GetButtonDown("Jump")) {
			rb.velocity = Vector3.up * jumpForce;
			grounded = false;
			boxEncounter = 1;
			//player_anim.SetInteger("State", 2); // jumping animation (need back to idle)
		}

		if (rb.velocity.y > 0.1 || rb.velocity.y < -0.1) {
			grounded = false;
		}
		// glide
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
		boxEncounter = 0;
	}
	void hopping(){
		if (boxEncounter == 0){
			transform.position += transform.up * hopHeight;	
		}
	}
	void FixedUpdate() {
		speed = rb.velocity.y;
		// player movement
		if (Input.GetAxis ("Horizontal") > 0) {
			transform.position += transform.right * Time.deltaTime * movementSpeed;
			TurnRight(1);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			transform.position += -transform.right * Time.deltaTime * movementSpeed;
			TurnLeft(1);
		} else if (Input.GetAxis ("Vertical") > 0 && !worldManager.mode2d) {
			transform.position += transform.forward * Time.deltaTime * movementSpeed;
			TurnRight(1);
		} else if (Input.GetAxis ("Vertical") < 0 && !worldManager.mode2d) {
			transform.position += -transform.forward * Time.deltaTime * movementSpeed;
			TurnLeft(1);
		} else {
			// back to idle
			if (left){
				TurnLeft(0);
			}else{
				TurnRight(0);
			}
		}
	}
	// Turn left with corresponded state
	void TurnLeft(int state){
		player_anim.SetInteger("State", state);
		if (left){ return ;}
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		left = true;
		right = false;
	}
	// Turn left with corresponded state
	void TurnRight(int state){
		player_anim.SetInteger("State", state);
		if (right){ return ;}
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		left = false;
		right = true;
	}


	// return true if player is allowed to jump again
	// (helps prevent infinite jumping)
	void OnCollisionStay(Collision col){
//		Debug.Log (rb.velocity.y);
//		Debug.Log (col.transform.tag);
//		if (col.transform.tag == "MeshDiff" || col.transform.tag == "ColliderDiff" || col.transform.tag == "Ground" || col.transform.tag == "2D" || col.transform.tag == "3D") {
		if ((rb.velocity.y < 0.1 && rb.velocity.y > -0.1) && (col.transform.tag == "MeshDiff" || col.transform.tag == "ColliderDiff" || col.transform.tag == "Ground" || col.transform.tag == "2D" || col.transform.tag == "3D")) {
			grounded = true;
			// Debug.Log ("Grounded again");
		} 
	}

	void OnCollisionEnter(Collision other) {
		// turn off hopping when collsion with box
		if (other.gameObject.CompareTag("Box")){
			boxEncounter = 1;
		}
	}
	void OnCollisionExit(Collision other) {
		// turn on hopping back on
		if (other.gameObject.CompareTag("Box")){
			boxEncounter = 0;
		}
	}

	// collecting the coin objects
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Coin")){
			Debug.Log("player touch coin");
			other.gameObject.SetActive(false);
			coinCount += 1;
			SetCountText();
		}

		if (coinCount >= 1 && other.gameObject.CompareTag ("Arch")) {
			// Teleport the User to the next scene
			Debug.Log("player touch arch");
			SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
			coinCount = 0;
			glidingEnabled = false;
		}
	}

	void SetCountText(){
		countText.text = "Coins collected: " + coinCount.ToString();
	}


}
