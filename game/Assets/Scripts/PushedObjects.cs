using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PushedObjects : MonoBehaviour {

	// require a rigid body (mass)
	private Rigidbody rb;
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
			Debug.Log("hit");
			ReturnDirection();
		}
		
    }
     
	private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
    void ReturnDirection(){
		HitDirection hitDirection = HitDirection.None;
		RaycastHit hit_1;
		RaycastHit hit_2;
		RaycastHit hit_3;
		RaycastHit hit_4;

		Ray ray_1 = new Ray(transform.position, Vector3.left);
		Ray ray_2 = new Ray(transform.position, Vector3.forward);
		Ray ray_3 = new Ray(transform.position, Vector3.right);
		Ray ray_4 = new Ray(transform.position, Vector3.back);

		if (Physics.Raycast(ray_1, out hit_1, 10f)){
			Debug.Log("hit 1");
		}
		if (Physics.Raycast(ray_2, out hit_2, 10f)){
			Debug.Log("hit 2");
		}
		if (Physics.Raycast(ray_3, out hit_3, 10f)){
			Debug.Log("hit 3");
		}
		if (Physics.Raycast(ray_4, out hit_4, 10f)){
			Debug.Log("hit 4");
		}

		//return hitDirection;
	}

}
