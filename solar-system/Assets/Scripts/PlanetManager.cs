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
