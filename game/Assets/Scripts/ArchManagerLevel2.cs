using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchManagerLevel2 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (PlayerController.coinCount >= 3) {
			Behaviour halo = (Behaviour)GetComponent("Halo");
			halo.enabled = true;
		}
	}
}
