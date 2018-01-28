using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioObject : MonoBehaviour {

    private Transform sceneParent;
    private GameObject waveEffect;
    private Vector2 maxSize;
    private Vector2 size;

	// Use this for initialization
	void Start () {
        sceneParent = transform.parent;
        waveEffect = transform.Find("waveEffect").gameObject;
        maxSize = waveEffect.transform.localScale;
        size = new Vector2(1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.parent == sceneParent) {
            size += (new Vector2(0, 0) - size) / 10;
        } else {
            size += (new Vector2(maxSize.x, maxSize.y) - size) / 10;
		}
		waveEffect.transform.localScale = size;
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.X)) {
            
        }
    }
}
