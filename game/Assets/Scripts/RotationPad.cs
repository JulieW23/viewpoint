using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPad : MonoBehaviour {

	public bool rotatable = false;
	public GameObject YButton;
	void OnCollisionStay(Collision collision){
		if (collision.gameObject.tag == "Player") {
			rotatable = true;
			YButton.SetActive(true);
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Player") {
			rotatable = false;
			YButton.SetActive(false);
		}
	}
}
