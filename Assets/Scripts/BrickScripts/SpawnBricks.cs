using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnBricks : MonoBehaviour {

	public GameObject brickPrefab; 
	public string level;
	public Vector3 start = new Vector3(0, 1, 0); 

	// Use this for initialization
	void Start () {
		string[] lines = File.ReadAllLines(Application.dataPath + "/Levels/" + level + ".txt");
		foreach(var line in lines ) { 
			foreach (char var in line){ 
				Instantiate(brickPrefab, start, Quaternion.identity);
				start = start + new Vector3(brickPrefab.GetComponent<Renderer>().bounds.size.z, 0, 0);
			}
			start = start + new Vector3(0, brickPrefab.GetComponent<Renderer>().bounds.size.z, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
