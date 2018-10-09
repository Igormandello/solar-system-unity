using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject target;

    [Range(0.1f, 5)]
    public float speed = 0.1f;

    void Start()
    { }


	void Update ()
    {
        this.transform.RotateAround(this.target.transform.position, Vector3.up, this.speed);
	}
}
