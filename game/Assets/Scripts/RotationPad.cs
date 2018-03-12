using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPad : MonoBehaviour {

	private Collider col;
	public bool rotatable = false;
	public Vector3 angle;
	public float duration;

	void Start () {
		col = gameObject.GetComponent<Collider>();
	}
		
	void OnCollisionStay(Collision collision){
		if (collision.gameObject.tag == "Player") {
			Transform player = collision.transform;
			player.parent = transform;
			rotatable = true;
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Player") {
			Transform player = collision.transform;
			player.parent = null;
			rotatable = false;
		}
	}
}
