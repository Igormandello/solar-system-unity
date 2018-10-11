﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject jupiterMoon;

    [Range(0.1f, 3)]
    public float scale = 1;

    private readonly Dictionary<string, float> speed = new Dictionary<string, float>
    {
        { "Sun", 2 },
        { "Mercury", 3 },
        { "Venus", 2 },
        { "Earth", 1.8f },
        { "Mars", 1.5f },
        { "Jupiter", 0.8f },
        { "Saturn", 0.6f },
        { "Uranus", 0.4f },
        { "Neptune", 0.3f }
    };

    private GameObject focused = null;
    private GameObject camera;

	void Start ()
    {
        this.camera = GameObject.Find("Main Camera");

        GameObject sun = GameObject.Find("Sun");
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        for (int i = 0; i < planets.Length; i++) {
            RotateAround rotateObj = planets[i].AddComponent<RotateAround>();
            rotateObj.target = sun;
            rotateObj.speed = speed[planets[i].name] * scale;

            PlanetController controller = planets[i].AddComponent<PlanetController>();
            controller.OnPlanetClick += FocusCamera;
            controller.speed = speed[planets[i].name] * scale;
        }

        PlanetController sunController = sun.AddComponent<PlanetController>();
        sunController.OnPlanetClick += FocusCamera;
        sunController.speed = speed["Sun"] * scale;

        GameObject jupiter = GameObject.Find("Jupiter");
        for (int n = 0; n < 79; n++)
        {
            GameObject g = Instantiate(jupiterMoon);
            g.transform.parent = jupiter.transform;
            float scale = Random.Range(0.02f, 0.1f);
            g.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            float multX = 1, multZ = 1;
            if (Random.Range(0, 1f) > 0.5f)
                multX = -1;

            if (Random.Range(0, 1f) > 0.5f)
                multZ = -1;

            g.transform.localPosition = new Vector3(multX * Random.Range(1, 0.5f), Random.Range(-0.6f, 0.6f), multZ * Random.Range(1, 0.5f));

            RotateAround rotateObj = g.AddComponent<RotateAround>();
            rotateObj.target = jupiter;
            rotateObj.speed = Random.Range(0.5f, 3f);
        }
    }

    private void Update()
    {
        if (this.focused != null)
        {
            Vector3 dir = this.focused.transform.position - this.camera.transform.position;
            this.camera.transform.rotation = Quaternion.RotateTowards(this.camera.transform.rotation, Quaternion.LookRotation(dir), 1);
        }
            
    }

    private void FocusCamera(GameObject planet)
    {
        this.focused = planet;
    }
}
