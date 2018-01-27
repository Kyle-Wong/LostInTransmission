using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveScript : MonoBehaviour {

    private float life = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.rotation * Vector3.down / 10;
        life -= Time.deltaTime;
        if (life<=0) {
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground"){
            Destroy(gameObject);
        }
    }
}
