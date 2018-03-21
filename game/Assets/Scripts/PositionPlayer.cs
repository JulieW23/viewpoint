using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlayer : MonoBehaviour {

	public float x = 0f;
	public float y = 0f;
	public float z = 0f;
	public bool disable = false;
	private Collider col;

	void Start(){
		col = gameObject.GetComponent<Collider>();
	}

	void OnTriggerStay(Collider collision){
		if (collision.gameObject.tag == "Player") {
			GameObject player = collision.gameObject;
//			Debug.Log ("move!");
			if (x != 0) {
				player.transform.position = new Vector3 (x, player.transform.position.y, player.transform.position.z);
			}
			if (y != 0) {
				player.transform.position = new Vector3 (player.transform.position.x, y, player.transform.position.z);
			}
			if (z != 0) {
				player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, z);
			}
			if (disable == true) {
				col.enabled = false;
			}
		}
	}

	void OnTriggerExit(Collider collision){
	
	}
}
