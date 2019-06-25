using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collisiion)
    {
        if (collisiion.gameObject.name == "Sphere")
        {
            Destroy(gameObject);
        }
    }
}
