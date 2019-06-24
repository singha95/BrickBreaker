using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour {

	// Use this for initialization
	public float ballSpeed;
	private Rigidbody ball;
	private Vector3 dir;  
	 
	void Start () {
		ball = GetComponent<Rigidbody>(); 
		dir = new Vector3(ballSpeed, 0, Random.Range(-ballSpeed, ballSpeed));
	}
	
	// Update is called once per frame
	void Update () {
		ball.velocity = dir; 
	}

	void OnCollisionEnter(Collision collisiion){ 
		print(collisiion.gameObject.name);
		switch(collisiion.gameObject.name){ 
			case "left": 
				dir.Scale(new Vector3(1, 1, -1f));
                gameObject.name = "Sphere";
                break; 
			case "right": 
				dir.Scale(new Vector3(1, 1, -1f));
                gameObject.name = "Sphere";
                break;  
			case "up": 
				dir.Scale(new Vector3(-1f, 1, 1f));
                gameObject.name = "Sphere";
                break; 
			case "down": 
				dir.Scale(new Vector3(0f, 0, 0f));
                gameObject.name = "Sphere";
                break;
			case "Player": 
				dir.Scale(new Vector3(-1, 1, Random.Range(0,3) * Random.Range(0, 2) * 2 - 1));
                gameObject.name = "Sphere";
                break;
            case "Brick(Clone)":
                dir.Scale(new Vector3(-1, 1, Random.Range(0, 3) * Random.Range(0, 2)*2 - 1 ));
                gameObject.name = "SphereHit";
                break;
            case "Plane": 
				break; 
		}
	}
}
