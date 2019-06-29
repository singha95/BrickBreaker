using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{

    // Use this for initialization
    public float ballSpeed;
    public Text score;
    public GameObject ballPrefab;
	public Text gameOver; 
    public static int LIVES = 3;
    
    private int gameScore = 0;
    private Rigidbody ball;
    private Vector3 dir;

    void Start()
    {
        ball = GetComponent<Rigidbody>();
        dir = new Vector3(ballSpeed, 0, Random.Range(-ballSpeed, ballSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        ball.velocity = dir;
    }

    void Reset()
    {
        //Set the public values before creating the new prefab 
        BallMovement.LIVES--;
        if (LIVES < 1)
        {
            dir.Scale(Vector3.zero);
			gameOver.enabled = true;
        }
        else
        {
            BallMovement newSphere = ballPrefab.GetComponent<BallMovement>();
            newSphere.score = this.score;
            newSphere.ballSpeed = this.ballSpeed;
            newSphere.ballPrefab = this.ballPrefab;
            Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    void HitBrick(GameObject brick){ 
        gameObject.name = "SphereHit";
        dir.Scale(new Vector3(-1, 1, Random.Range(0, 2) * 2 - 1));
        Destroy(brick);
        gameObject.name = "Sphere";
        gameScore += 10;
        score.text = "Score: " + gameScore;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.name)
        {
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
                Reset();
                break;
            case "Player":
                dir.Scale(new Vector3(-1, 1, ballSpeed * 0.25f));
                break;
            case "Brick(Clone)":
                HitBrick(collision.gameObject);
                break;
            case "Plane":
                break;
        }
    }
}
