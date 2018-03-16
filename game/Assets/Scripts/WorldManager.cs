using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	[Header("Self")]
	public static WorldManager instance; // reference to self

	[Header("Init Settings")]
	public bool mode2d; // true if 2d, false if 3d

	[Header ("World Orientation")]
	public float front = 0f;
	public float left = 90f;
	public float right = -90f;
	public float back = 180f;

	// array of all objects that need colliders altered when view switches
	private GameObject[] changeCol; 
	// array of all objects that need mesh altered when view switches
	private GameObject[] changeMesh;

	void Awake(){
		// make sure there is only once instance of WorldManager
		if (instance != null) {
			Debug.LogError ("There is already a WorldManager in this scene.");
			return;
		}
		instance = this;
	}

	void Start () {

		// get objects by tag
		changeCol = GameObject.FindGameObjectsWithTag("ColliderDiff");
		changeMesh = GameObject.FindGameObjectsWithTag ("MeshDiff");

		// use the correct colliders
		if (!mode2d) { //3d
			// ***** COLLIDER DIFF *****
			// for all objects that have different colliders in each mode
			foreach (GameObject obj in changeCol) {
				// get the collider of the object
				Collider col = obj.GetComponent<Collider> ();
				// get all the colliders of the children of this object
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();

				foreach (Collider c in child_col) {
					if (c.gameObject.tag == "2D" || c.gameObject.tag == "2D") {
						c.enabled = false;
					} else if (c.gameObject.tag == "3D") {
						c.enabled = true;
					}
				}
				col.enabled = true;
			}
			// ***** MESH DIFF *****
			// for all objects that have different mesh in each mode
			foreach (GameObject obj in changeMesh) {
				// get the collider of the object
				Collider col = obj.GetComponent<Collider> ();
				// get the mesh renderer of the object
				Renderer ren = obj.GetComponent<Renderer> ();
				// get all the colliders of the children of this object
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();
				// get all the mesh renderers of the children of this object
				Renderer[] child_ren = obj.GetComponentsInChildren<Renderer> ();

				for (int i = 0; i < child_col.Length; i++) {
					if (child_col[i].gameObject.tag == "2D") {
						child_col[i].enabled = false;
					}
					else if (child_col[i].gameObject.tag == "3D") {
						child_col[i].enabled = true;
					}
					else {
						child_col [i].enabled = false;
						child_ren [i].enabled = false;
					}
				}
				col.enabled = true;
				ren.enabled = true;
			}
				
		} else if (mode2d) { //2d
			// ***** COLLIDER DIFF *****
			foreach (GameObject obj in changeCol) {
				Collider col = obj.GetComponent<Collider> ();
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();

				foreach (Collider c in child_col) {
					if (c.gameObject.tag == "2D") {
						c.enabled = true;
					} else if (c.gameObject.tag == "3D") {
						c.enabled = false;
					}
				}
				col.enabled = false;
			}
			// ***** MESH DIFF *****
			foreach (GameObject obj in changeMesh) {
				// get the collider of the object
				Collider col = obj.GetComponent<Collider> ();
				// get the mesh renderer of the object
				Renderer ren = obj.GetComponent<Renderer> ();
				// get all the colliders of the children of this object
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();
				// get all the mesh renderers of the children of this object
				Renderer[] child_ren = obj.GetComponentsInChildren<Renderer> ();

				for (int i = 0; i < child_col.Length; i++) {
					if (child_col[i].gameObject.tag == "2D") {
						child_col[i].enabled = true;
					}
					else if (child_col[i].gameObject.tag == "3D") {
						child_col[i].enabled = false;
					}
					else {
						child_col [i].enabled = true;
						child_ren [i].enabled = true;
					}
				}
				col.enabled = false;
				ren.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// switch 2d/3d mode when key is pressed
		if (Input.GetButtonDown ("Change Perspective")) {
			FindObjectOfType<AudioManager>().Play("SwitchPerspective");
			// update mode
			mode2d = !mode2d;

			// colliders
			foreach (GameObject obj in changeCol) {
				Collider col = obj.GetComponent<Collider> ();
				Collider[] all_col = obj.GetComponentsInChildren<Collider> ();
				foreach (Collider c in all_col) {
					if (c.gameObject.tag == "2D") {
						c.enabled = mode2d;
					} else if (c.gameObject.tag == "3D") {
						c.enabled = !mode2d;
					}
				}
				col.enabled = !col.enabled;
			}
			// mesh
			foreach (GameObject obj in changeMesh){
				Collider[] all_col = obj.GetComponentsInChildren<Collider> ();
				Renderer[] all_ren = obj.GetComponentsInChildren<Renderer> ();
				for (int i = 0; i < all_col.Length; i++) {
					if (all_col [i].gameObject.tag == "2D") {
						all_col [i].enabled = mode2d;
					}
					else if (all_col [i].gameObject.tag == "3D") {
						all_col [i].enabled = !mode2d;
					}
					else {
						all_col [i].enabled = !all_col [i].enabled;
						all_ren [i].enabled = !all_ren [i].enabled;
					}
				}
			}
		}
	}
}
