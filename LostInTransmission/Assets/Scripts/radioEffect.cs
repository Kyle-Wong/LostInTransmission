using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioEffect : MonoBehaviour {

    private Vector2 size;
	// Use this for initialization
	void Start () {
        size = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,6));
        float scale = 1+Mathf.Cos(Time.time * 4)/10;
        transform.localScale = new Vector2(size.x * scale, size.y * scale);
	}
}
