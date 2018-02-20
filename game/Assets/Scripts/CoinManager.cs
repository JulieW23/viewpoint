using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CoinManager : MonoBehaviour {
	WorldManager worldManager;
	// Coins need to switch between 2D/3D position
	private Vector3 coin3D_pos;
	private Vector3 coin3D_collider;
	[SerializeField] private Vector3 coin2D_pos; 
	[SerializeField] private Vector3 coin2D_collider;
	BoxCollider coinCollider;
	GameObject coin;
	public float timeTakenDuringLerp2D = 0.9f;
	public float timeTakenDuringLerp3D = 1f;
	private float timeStartedLerping;
	void Start (){
		worldManager = WorldManager.instance;
		coin = transform.GetChild(0).gameObject; //get the child coin inside
		coinCollider = GetComponent<BoxCollider>();
		coin3D_pos = transform.position;
		coin3D_collider = coinCollider.size;
	}
	void FixedUpdate () {
		if (!worldManager.mode2d){ // in 3d world
			coinCollider.size = coin3D_collider;
			// transform animation
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp2D;
			transform.position = Vector3.Lerp(coin2D_pos, coin3D_pos, percentageComplete);
		} else { //in 2d world
			coinCollider.size = coin2D_collider;
			// transform animation
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp3D;
			transform.position = Vector3.Lerp(coin3D_pos, coin2D_pos, percentageComplete);
		}
	}
	void Update () {
		coin.transform.Rotate(new Vector3(0, 0, 70) * Time.deltaTime);
		if (Input.GetButtonDown ("Change Perspective")) {
			timeStartedLerping = Time.time;
		}
	}
}