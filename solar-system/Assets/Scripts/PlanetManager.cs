using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject jupiterMoon, meteor;

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
    private AudioSource audio = null;

	void Start ()
    {
        this.camera = GameObject.Find("Main Camera");

        GameObject sun = GameObject.Find("Sun");
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        camera.transform.LookAt(sun.transform);

        for (int i = 0; i < planets.Length; i++) {
            RotateAround rotateObj = planets[i].AddComponent<RotateAround>();
            rotateObj.target = sun;
            rotateObj.speed = speed[planets[i].name] * scale;

            PlanetController controller = planets[i].AddComponent<PlanetController>();
            controller.OnPlanetClick += FocusCamera;
            controller.speed = speed[planets[i].name] * scale;
        }

        GameObject moon = GameObject.Find("Moon");
        PlanetController moonController = moon.AddComponent<PlanetController>();
        moonController.OnPlanetClick += FocusCamera;
        moonController.speed = 2 * scale;

        PlanetController sunController = sun.AddComponent<PlanetController>();
        sunController.OnPlanetClick += FocusCamera;
        sunController.speed = speed["Sun"] * scale;

        GameObject jupiter = GameObject.Find("Jupiter");
        for (int n = 0; n < 79; n++)
        {
            GameObject g = Instantiate(jupiterMoon);
            g.transform.parent = jupiter.transform;
            float scale = Random.Range(0.01f, 0.05f);
            g.transform.localScale = new Vector3(scale, scale, scale);

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

        for (int n = 0; n < 200; n++)
        {
            GameObject g = Instantiate(meteor);

            float z = Random.Range(-40, 40f);
            float x = ResolveCircle(z);

            g.transform.position = new Vector3(x, 0, z);

            RotateAround rotateObj = g.AddComponent<RotateAround>();
            rotateObj.target = sun;
            rotateObj.speed = Random.Range(0.5f, 3f);
        }
    }

    private void Update()
    {
        if (this.focused != null)
        {
            Vector3 dir = this.focused.transform.position - this.camera.transform.position;
            this.camera.transform.rotation = Quaternion.RotateTowards(this.camera.transform.rotation, Quaternion.LookRotation(dir), 1.5f);
        }
            
    }

    private void FocusCamera(GameObject planet)
    {
        this.focused = planet;

        if (this.audio != null)
            this.audio.Stop();

        this.audio = planet.GetComponent<AudioSource>();
        if (this.audio != null)
            audio.Play();
    }
    
    private float ResolveCircle(float z)
    {
        float powered = 1400 - Random.Range(-300, 50f) - Mathf.Pow(z, 2);
        if (powered < 0)
            return 0;
        else if (Random.Range(0, 1f) < 0.5f)
            return Mathf.Sqrt(powered);
        else
            return -Mathf.Sqrt(powered);
    }
}
