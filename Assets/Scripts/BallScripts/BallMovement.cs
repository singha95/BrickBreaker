using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BallMovement : MonoBehaviour {

	// Use this for initialization
	public float ballSpeed;
	public Text score; 

	private int gameScore = 0; 
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
                break; 
			case "right": 
				dir.Scale(new Vector3(1, 1, -1f));
                break;  
			case "up": 
				dir.Scale(new Vector3(-1f, 1, 1f));
                break; 
			case "down": 
				dir.Scale(new Vector3(0f, 0, 0f));
                break;
			case "Player": 
				dir.Scale(new Vector3(-1, 1,  Random.Range(0, 2) * 2 - 1));
                break;
            case "Brick(Clone)":
				gameObject.name = "SphereHit";
                dir.Scale(new Vector3(-1, 1, Random.Range(0, 2)*2 - 1 ));
				gameObject.name = "Sphere";
				gameScore += 10;
				score.text = "Score: " + gameScore;
                break;
            case "Plane": 
				break; 
		}
	}
}
