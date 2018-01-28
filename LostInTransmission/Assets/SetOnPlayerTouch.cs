using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnPlayerTouch : MonoBehaviour {
	private State state;
	void Awake() {
		state = GetComponent<State>();
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player")
			state.setState(state.getState() ^ 1);
	}
}
