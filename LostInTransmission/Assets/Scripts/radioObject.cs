using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioObject : MonoBehaviour {

    public LayerMask hitByLayer;
    private Transform sceneParent;
	// Use this for initialization
	void Start () {
        sceneParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.X)) {
            transform.parent = sceneParent;
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hitByLayer == (hitByLayer | (1 << collision.gameObject.layer)))
		{
            transform.position = collision.gameObject.transform.position + new Vector3(-.5f*collision.gameObject.transform.localScale.x, .5f, 0);
			transform.parent = collision.gameObject.transform;
		}
	}
}
