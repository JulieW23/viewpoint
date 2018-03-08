using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerController.coinCount >= 2) {
			Behaviour halo = (Behaviour)GetComponent("Halo");
			halo.enabled = true;
		}
	}
}
