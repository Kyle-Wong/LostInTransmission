using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioWaveEmitter : MonoBehaviour {

    // Use this for initialization
    public float emissionRate;
    public Transform prefab;
    public bool isEmitting;
    private float emissionTimer;
	void Start () {
        emissionTimer = 1f / emissionRate;
    }
	
	// Update is called once per frame
	void Update () {
        if (isEmitting)
        {
            if(emissionTimer > 0)
            {
                emissionTimer -= Time.deltaTime;
            } else
            {
                spawnWave();
                emissionTimer = 1f/emissionRate;
            }
        }
	}
    private void spawnWave()
    {
        GameObject wave = Instantiate(prefab,transform).gameObject;
    }
}
