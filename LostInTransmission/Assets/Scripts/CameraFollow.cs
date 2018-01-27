using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public Transform currentPlayerTransform;

	void Awake() {
		Debug.Assert(currentPlayerTransform);
	}
	
	// Update is called once per frame
	void Update () {
		var playerPos = currentPlayerTransform.position;
		transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);
	}
}
