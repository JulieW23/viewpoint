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

	[Header("Other player settings")]
	public float fallThreshold; // how far the player falls before respawning
	private Vector3 startGravity;
	public float hopHeight = 0.2f;
	public static int coinCount; // count the coins collected
	public Text countText;
	public Text orbLeftText;
	public int orbsToPass = 2;
	private Rigidbody rb; // reference to rigidbody of this player
	private bool grounded = true; // true iff player is not in the air

	WorldManager worldManager; // reference to WorldManager
	Animator player_anim;
	private bool left, right; // animation direction
	private bool play = false;
	private bool sound = false;
	private bool quit = false;
	private GameObject soundLight;
	private Behaviour soundPointLight;

	void Start() {
		worldManager = WorldManager.instance; // set the reference to WorldManager instance
		rb = GetComponent<Rigidbody> (); // get the rigidbody of this player object
		startGravity = Physics.gravity;
		coinCount = 0;
		SetCountText();
		player_anim = GetComponent<Animator>(); //get animator for animation
		if (player_anim == null){
			Debug.Log("has animator!");
		}
		right = true;
	}

	void Update () {
		// jump
		if (grounded && Input.GetButtonDown("Jump")) {
			rb.velocity = Vector3.up * jumpForce;
			grounded = false;
			player_anim.SetInteger("State", 2); // jumping animation (need back to idle)
		}

		if (rb.velocity.y > 8 || rb.velocity.y < -8) {
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
			Application.LoadLevel(Application.loadedLevel);
		}
			
		// Play button selected on the main menu
		if (play) {
			if (Input.GetButtonDown("Select")) {
				SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
			}
		}
			
		// Quit button selected on the main menu
		if (quit) {
			if (Input.GetButtonDown ("Select")) {
				Application.Quit();
			}
		}
			
		// Sound button selected on the main menu.
		if (sound) {
			if (Input.GetButtonDown ("Select")) {
				soundLight = GameObject.FindWithTag ("SoundLight");
				soundPointLight = (Behaviour)soundLight.GetComponent ("Light");
				soundPointLight.enabled = !soundPointLight.enabled;
				AudioListener.pause = !AudioListener.pause;
			}
		}
	}
	void FixedUpdate() {
		// player movement
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
			if (Input.GetAxis ("Horizontal") > 0) {
				transform.position += transform.right * Time.deltaTime * movementSpeed;
				TurnRight(1);
			} if (Input.GetAxis ("Horizontal") < 0) {
				transform.position += -transform.right * Time.deltaTime * movementSpeed;
				TurnLeft(1);
			} if (Input.GetAxis ("Vertical") > 0 && !worldManager.mode2d) {
				transform.position += transform.forward * Time.deltaTime * movementSpeed;
				TurnRight(1);
			} if (Input.GetAxis ("Vertical") < 0 && !worldManager.mode2d) {
				transform.position += -transform.forward * Time.deltaTime * movementSpeed;
			    TurnLeft(1);
			} 
		}else {
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
		if ((rb.velocity.y < 8 && rb.velocity.y > -8) && (col.transform.tag == "MeshDiff" || 
			col.transform.tag == "ColliderDiff" || col.transform.tag == "Ground" || 
			col.transform.tag == "2D" || col.transform.tag == "3D" || 
			col.transform.tag == "Front" || 
			col.transform.tag == "Left" || col.transform.tag == "Right" || col.transform.tag == "Back" || 
			col.transform.tag == "RotatePad" || col.transform.tag == "play" || col.transform.tag == "soundButton" ||
			col.transform.tag == "quitButton")) {
			grounded = true;
			// Debug.Log ("Grounded again");
		}

		if (col.gameObject.CompareTag("play")) {
			Debug.Log("touch play");
			play = true;
			sound = false;
			quit = false;
		}

		if (col.gameObject.CompareTag ("soundButton")) {
			play = false;
			sound = true;
			quit = false;
		}

		if (col.gameObject.CompareTag ("quitButton")) {
			play = false;
			sound = false;
			quit = true;
		}
	}



	// collecting the coin objects
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Coin")){
			Debug.Log("player touch coin");
			other.gameObject.SetActive(false);
			coinCount += 1;
			SetCountText();
			FindObjectOfType<AudioManager>().Play("OrbCollect");
		}

		if (coinCount >= orbsToPass && other.gameObject.CompareTag ("Arch")) {
			// Teleport the User to the next scene
			Debug.Log ("player touch arch");
			Debug.Log (coinCount.ToString ());
			SceneManager.LoadScene ("Level2", LoadSceneMode.Single);
			coinCount = 0;
			orbsToPass = 3;
		}

		if (coinCount >= orbsToPass && other.gameObject.CompareTag("Arch2")) {
			Debug.Log ("player touch arch");
			SceneManager.LoadScene ("Level3", LoadSceneMode.Single);
			coinCount = 0;
			orbsToPass = 1;
		}

		if (other.gameObject.CompareTag("play")) {
			Debug.Log("touch play");
			play = true;
		}

		if (other.gameObject.CompareTag ("soundButton")) {
			sound = true;
		}

		if (other.gameObject.CompareTag ("quitButton")) {
			quit = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("play")) {
			play = false;
		}

		if (other.gameObject.CompareTag ("soundButton")) {
			sound = false;
		}

		if (other.gameObject.CompareTag("quitButton")) {
			quit = false;
		}
	}

	void SetCountText(){
		int orbLeft = orbsToPass-coinCount;
		countText.text = "Orbs collected: " + coinCount.ToString();
		if (orbLeft < 0){
			orbLeft = 0;
		}
		orbLeftText.text = "To unlock the portal: " + orbLeft.ToString();
	}

}
