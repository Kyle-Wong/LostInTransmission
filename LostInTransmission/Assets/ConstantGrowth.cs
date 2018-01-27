using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantGrowth : MonoBehaviour {

    // Use this for initialization
    public float growthRate;
    private Vector3 growthVector;
	void Start () {
        growthVector = new Vector3(1, 1, 0) * growthRate;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale += growthVector * Time.deltaTime;
	}
}
