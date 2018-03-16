using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPad : MonoBehaviour {

	public bool rotatable = false;
		
	void OnCollisionStay(Collision collision){
		if (collision.gameObject.tag == "Player") {
			rotatable = true;
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Player") {
			rotatable = false;
		}
	}
}
