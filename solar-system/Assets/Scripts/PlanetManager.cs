using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [Range(0.1f, 3)]
    public const int scale = 1;

    private readonly Dictionary<string, float> speed = new Dictionary<string, float>
    {
        { "Mercury", 3 },
        { "Venus", 2 },
        { "Earth", 1.8f },
        { "Mars", 1.5f },
        { "Jupiter", 0.8f },
        { "Saturn", 0.6f },
        { "Uranus", 0.4f },
        { "Neptune", 0.3f }
    };

	void Start ()
    {
        GameObject camera = GameObject.Find("Main Camera");

        GameObject sun = GameObject.Find("Sun");
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        for (int i = 0; i < planets.Length; i++) {
            RotateAround rotateObj = planets[i].AddComponent<RotateAround>();
            rotateObj.target = sun;
            rotateObj.speed = speed[planets[i].name] * scale;

            PlanetController controller = planets[i].AddComponent<PlanetController>();
            controller.camera = camera;
            controller.speed = speed[planets[i].name] * scale;
        }
	}
}
