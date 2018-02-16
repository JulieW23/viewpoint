using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionObject : MonoBehaviour {

	[SerializeField] private Vector3 pos3D; 
	[SerializeField] private Vector3 pos2D; 

	WorldManager worldManager;

	// Use this for initialization
	void Start () {
		worldManager = WorldManager.instance;

		if (worldManager.mode2d) {
			transform.position = pos2D;
		} else {
			transform.position = pos3D;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (worldManager.mode2d) {
			transform.position = pos2D;
		} else {
			transform.position = pos3D;
		}
	}

	void OnCollisionStay(Collision collisionInfo){
		if (collisionInfo.gameObject.tag == "Player") {
			Transform player = collisionInfo.transform;
			player.parent = transform;
		}
	}

	void OnCollisionExit(Collision collisionInfo){
		if (collisionInfo.gameObject.tag == "Player") {
			Transform player = collisionInfo.transform;
			player.parent = null;
		}
	}
}
