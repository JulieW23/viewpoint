using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionObject : MonoBehaviour {

	[SerializeField] private Vector3 pos3D; 
	[SerializeField] private Vector3 pos2D; 
	public float timeTakenDuringLerp2D = 0.9f;
	public float timeTakenDuringLerp3D = 1f;
	private float timeStartedLerping;

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

	void FixedUpdate(){
		if (worldManager.mode2d) {
			if(transform.position != pos2D){
				float timeSinceStarted = Time.time - timeStartedLerping;
				float percentageComplete = timeSinceStarted / timeTakenDuringLerp2D;
				transform.position = Vector3.Lerp(pos3D, pos2D, percentageComplete);
			}
		} else {
			if (transform.position != pos3D){
				float timeSinceStarted = Time.time - timeStartedLerping;
				float percentageComplete = timeSinceStarted / timeTakenDuringLerp3D;
				transform.position = Vector3.Lerp(pos2D, pos3D, percentageComplete);
			}
		}
	}
	void Update () {
		if (Input.GetButtonDown ("Change Perspective")) {
			timeStartedLerping = Time.time;
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
