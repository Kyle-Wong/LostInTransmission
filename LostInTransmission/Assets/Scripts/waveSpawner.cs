using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour {

    public GameObject prefab;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.X)) {
			for (int i = 0; i < 360; i++)
			{
				Instantiate(prefab, transform.position, Quaternion.Euler(0, 0, i));
			}
        }
	}
}
