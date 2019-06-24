using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnBricks : MonoBehaviour {

	public GameObject brickPrefab; 
	public string level;
	public Vector3 start;
    private Vector3 current;

	// Use this for initialization
	void Start () {
        current = start;
        int count = 1; 
		string[] lines = File.ReadAllLines(Application.dataPath + "/Levels/" + level + ".txt");
		foreach(var line in lines ) { 
			foreach (char var in line){ 
				Instantiate(brickPrefab, current, Quaternion.identity);
				current = current + new Vector3(0, 0, brickPrefab.GetComponent<Renderer>().bounds.size.z);
			}
			current = start + new Vector3(0, brickPrefab.GetComponent<Renderer>().bounds.size.z * count, 0);
            count += 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
