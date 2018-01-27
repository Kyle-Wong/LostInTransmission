using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendTest : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		Debug.Assert(rb);
	}
	void Start() {
		rb.AddForce(new Vector2(100, 200));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
