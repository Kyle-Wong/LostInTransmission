using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioObject : MonoBehaviour {

	public LayerMask hitByLayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hitByLayer == (hitByLayer | (1 << collision.gameObject.layer)))
		{
            transform.parent = collision.gameObject.transform;
		}
	}
}
