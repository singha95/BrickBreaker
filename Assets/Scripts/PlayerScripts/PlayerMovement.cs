using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	Rigidbody player; 
	string[] wallNames = { "left", "right", "up", "down" };

	void Start () {
		player = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")){ 
			player.velocity += Vector3.back * 1f;
		}

		if (Input.GetKey("right")){ 
			player.velocity += Vector3.forward * 1f;
		}
		
	}

	void OnCollisionEnter(Collision collisiion){ 
		if (wallNames.Contains(collisiion.gameObject.name )){
			//print("Collision: " + collisiion.gameObject.name);
			player.velocity = Vector3.zero; 
		}
	}
}
