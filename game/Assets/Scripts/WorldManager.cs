using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	[Header("Self")]
	public static WorldManager instance;

	[Header("Init Settings")]
	public bool mode2d; // true if 2d, false if 3d

	[Header("Objects")]
	public Camera camera2d;
	public Camera camera3d;

	void Awake(){
		if (instance != null) {
			Debug.LogError ("There is already a WorldManager in this scene.");
			return;
		}
		instance = this;
	}

	void Start () {
		camera2d.enabled = mode2d;
		camera3d.enabled = !mode2d;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			mode2d = !mode2d;
			camera2d.enabled = !camera2d.enabled;
			camera3d.enabled = !camera3d.enabled;
		}
	}
}
