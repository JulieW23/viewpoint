using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	[Header("Self")]
	public static WorldManager instance; // reference to self

	[Header("Init Settings")]
	public bool mode2d; // true if 2d, false if 3d

	[Header("Objects")]
	public Camera camera2d; // ref to 2d cam
	public Camera camera3d; // ref to 3d cam
//	public GameObject world2d;
//	public GameObject world3d;

	// array of all objects that need colliders altered when view switches
	private GameObject[] changeCol; 

	void Awake(){
		// make sure there is only once instance of WorldManager
		if (instance != null) {
			Debug.LogError ("There is already a WorldManager in this scene.");
			return;
		}
		instance = this;
	}

	void Start () {
		// enable the correct camera
		camera2d.enabled = mode2d;
		camera3d.enabled = !mode2d;

		// get objects by tag
		changeCol = GameObject.FindGameObjectsWithTag("ColliderDiff");


		// use the correct colliders
		if (!mode2d) { //3d
			foreach (GameObject obj in changeCol) {
				Collider col = obj.GetComponent<Collider> ();
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();

				foreach (Collider c in child_col) {
					c.enabled = false;
				}
				col.enabled = true;
			}
//			world3d.SetActive(true);
//			world2d.SetActive (false);

		} else if (mode2d) { //2d
			foreach (GameObject obj in changeCol) {
				Collider col = obj.GetComponent<Collider> ();
				Collider[] child_col = obj.GetComponentsInChildren<Collider> ();

				foreach (Collider c in child_col) {
					c.enabled = true;
				}
				col.enabled = false;
			}
//			world2d.SetActive(true);
//			world3d.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// switch 2d/3d mode when key is pressed
		if (Input.GetKeyDown (KeyCode.C)) {
			// update mode
			mode2d = !mode2d;

			// camera
			camera2d.enabled = !camera2d.enabled;
			camera3d.enabled = !camera3d.enabled;

			// colliders
			foreach (GameObject obj in changeCol) {
				Collider[] all_col = obj.GetComponentsInChildren<Collider> ();
				foreach (Collider c in all_col) {
					c.enabled = !c.enabled;
				}
			}
//			world2d.SetActive(!world2d.activeSelf);
//			world3d.SetActive (!world3d.activeSelf);
		}
	}
}
