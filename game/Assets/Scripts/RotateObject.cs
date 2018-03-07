using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	public float range; // player must be within this range to rotate this object
	public GameObject player; // reference to player
	Behaviour halo; // reference to halo
	public Vector3 angle; // rotation angle
	public float duration; // rotation duration
	public bool showIndicator = false;

	void Start () {
		halo = (Behaviour)GetComponent ("Halo"); // set halo reference
	}

	void Update () {
		// check if player is in range
		float distanceFromPlayer = Vector3.Distance (transform.position, player.transform.position);
		// if player is in range
		if (distanceFromPlayer <= range) {
			// turn on rotation indicator
			halo.enabled = true;
			// if player is in range and rotation key is pressed, rotate the object
			if (Input.GetKeyDown (KeyCode.R)) {
				Debug.Log ("rotate!");
				StartCoroutine(rotateObject(angle, duration));
			}
		} 
		// if player is not in range
		else {
			// turn off rotation indicator
			halo.enabled = false;
		}
	}

	// indicate range in scene for easier adjustments
	void OnDrawGizmosSelected(){
		if (showIndicator) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, range);
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
		rotating = false;
	}
}
