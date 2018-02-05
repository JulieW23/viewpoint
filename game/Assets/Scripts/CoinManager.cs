using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {
	WorldManager worldManager;
	// Coins need to switch between 2D/3D position
	private Vector3 coin3D_pos;
	[SerializeField] private Vector3 coin2D_pos; 
	void Start (){
		worldManager = WorldManager.instance;
		coin3D_pos = transform.position;
	}
	
	void Update () {
 		transform.Rotate(new Vector3(0, 70, 0) * Time.deltaTime);
		if (!worldManager.mode2d) { // 3d
			transform.position = coin3D_pos;
		} else {
			transform.position = coin2D_pos;
		}
	}
}