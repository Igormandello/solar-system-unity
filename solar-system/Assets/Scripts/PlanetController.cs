using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public delegate void PlanetClicked(GameObject obj);
    public event PlanetClicked OnPlanetClick;

    public float speed;
	
	void Update ()
    {
        this.transform.Rotate(Vector3.up, speed);
	}

    private void OnMouseDown()
    {
        if (OnPlanetClick != null)
            OnPlanetClick(this.gameObject);
    }
}
