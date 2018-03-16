using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	public Vector3 angle; // rotation angle
	public float duration; // rotation duration
	private int direction = 1;
	WorldManager worldManager;
	private Collider[] children;
	public RotationPad rotationPad;
	public ParticleSystem indicator;

	void Start () {
		worldManager = WorldManager.instance;
		children = gameObject.GetComponentsInChildren<Collider> ();
		indicator.Pause ();
	}

	void Update () {
		if (rotationPad.rotatable) {
			indicator.Play ();
			if (Input.GetButtonDown ("Rotate") && !worldManager.mode2d) {
				FindObjectOfType<AudioManager>().Play("Rotation");
				Debug.Log ("rotate!");
				StartCoroutine (rotateObject (angle, duration));
			}
		} else {
			indicator.Pause ();
			indicator.Clear();
		}

		// handle colliders
		if (worldManager.mode2d) {
			for (int i = 0; i < children.Length; i++) {
				if (direction == 1 && children [i].gameObject.tag == "Front") {
					children [i].enabled = true;
				} else if (direction == 2 && children [i].gameObject.tag == "Left") {
					children [i].enabled = true;
				} else if (direction == 3 && children [i].gameObject.tag == "Back") {
					children [i].enabled = true;
				} else if (direction == 4 && children [i].gameObject.tag == "Right") {
					children [i].enabled = true;
				} else {
					if (children [i].gameObject.tag != "RotatePad") {
						children [i].enabled = false;
					}
				}
			}
		} else {
			for (int i = 0; i < children.Length; i++) {
				if (children [i].gameObject.tag != "RotatePad") {
					children [i].enabled = false;
				}
			}
			Collider selfCol = gameObject.GetComponent<Collider> ();
			selfCol.enabled = true;
		}
	}

	// rotates object
	bool rotating = false;
	IEnumerator rotateObject(Vector3 eulerAngles, float duration)
	{
		if (rotating)
		{
			yield break;
		}
		rotating = true;

		Vector3 newRot = transform.eulerAngles + eulerAngles;

		Vector3 currentRot = transform.eulerAngles;

		float counter = 0;
		while (counter < duration)
		{
			counter += Time.deltaTime;
			transform.eulerAngles = Vector3.Lerp(currentRot, newRot, counter / duration);
			yield return null;
		}
		if (direction >= 1 && direction <= 3) {
			direction += 1;
		} else if (direction == 4) {
			direction = 1;
		}
		Debug.Log (direction);
		transform.eulerAngles = newRot;
		rotating = false;
	}
}
