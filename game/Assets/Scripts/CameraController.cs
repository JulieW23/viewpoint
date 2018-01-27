using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Camera camera2d;
	public Camera camera3d;

	// Use this for initialization
	void Start () {
		camera2d.enabled = true;
		camera3d.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			camera2d.enabled = !camera2d.enabled;
			camera3d.enabled = !camera3d.enabled;
		}
	}
}
