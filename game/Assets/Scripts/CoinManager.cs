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
	void Start (){
		worldManager = WorldManager.instance;
		coin = transform.GetChild(0).gameObject; //get the child coin inside
		coinCollider = GetComponent<BoxCollider>();
		coin3D_pos = transform.position;
		coin3D_collider = coinCollider.size;
	}
	
	void Update () {
		coin.transform.Rotate(new Vector3(0, 0, 70) * Time.deltaTime);
		if (!worldManager.mode2d){ // in 3d world
			transform.position = coin3D_pos;
			coinCollider.size = coin3D_collider;
		} else { //in 2d world
			transform.position = coin2D_pos;
			coinCollider.size = coin2D_collider;
		}
	}
}