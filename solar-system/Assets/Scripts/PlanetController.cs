using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject camera;
    public float speed;
	
	void Update ()
    {
        this.transform.Rotate(Vector3.up, speed);
	}
}
