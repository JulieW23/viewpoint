using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlayer : MonoBehaviour {

	public float z;

	void OnTriggerStay(Collider collision){
		if (collision.gameObject.name == "Player") {
			GameObject player = collision.gameObject;
			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, z);
		}
	}
}
