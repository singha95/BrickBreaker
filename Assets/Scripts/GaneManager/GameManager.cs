using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null; 
	public SpawnBricks brickSpawner; 
	public string level = "lvl1";
	public Vector3 start; 

	void Awake() { 
		brickSpawner = GetComponent<SpawnBricks>();
		if (instance == null){ 
			instance = this; 
		}else if (instance != this){ 
			Destroy(gameObject);
		}
		//Ensureds this gameObject is not destropyed when loading a scene 
		DontDestroyOnLoad(gameObject);
		brickSpawner.LoadLevel(level, start);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
