using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    private readonly float[] speed = new float[]
    {
        3, 2, 1.8f, 1.5f, 0.8f, 0.6f, 0.4f, 0.3f
    };

	void Start ()
    {
        GameObject sun = GameObject.Find("Sun");
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        for (int i = 0; i < planets.Length; i++) {
            RotateAround rotateObj = planets[i].AddComponent<RotateAround>();
            rotateObj.target = sun;
            rotateObj.speed = speed[planets.Length - i - 1];
        }
	}
}
