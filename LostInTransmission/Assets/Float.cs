using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    // Use this for initialization
    private Vector3 sourcePoint;
    public float amplitude;
    public float speed;
	void Start () {
        sourcePoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = sourcePoint + Vector3.up * amplitude * Mathf.Sin(Time.time*speed);
	}
}
