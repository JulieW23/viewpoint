using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchManager : MonoBehaviour {
	public static ArchManager instance;
	public int minCoins;
	
	// Update is called once per frame
	void Update () {
		if (PlayerController.coinCount >= minCoins) {
			Behaviour halo = (Behaviour)GetComponent("Halo");
			halo.enabled = true;
		}
	}
}
