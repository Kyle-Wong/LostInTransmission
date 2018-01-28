using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	//NOTE: you will need to query Kyle's delay
	// Use this for initialization
	private Transform currentPlayerTransform;
	private PlayerScript [] players = new PlayerScript[2];
	public int speed;
	private float interval;
	private static CameraFollow _instance;
	private static bool isFollowing = true;

	void Awake() {
		GameObject[] player_objs = GameObject.FindGameObjectsWithTag("Player");
		Debug.Assert(player_objs.Length == 2);
		for(int i = 0; i < 2; ++i) {
			players[i] = player_objs[i].GetComponent<PlayerScript>();
			Debug.Assert(players[i] != null);
		}
		Debug.Assert(speed != 0);
		interval = 1 / speed;
		currentPlayerTransform = players[0].transform;
		Debug.Assert(currentPlayerTransform);
	}
	
	// Update is called once per frame
	void Update () {
		updatePlayer();
		Vector2 playerPos = currentPlayerTransform.position;
		Vector2 cameraPos = transform.position;

		Vector2 delta = (playerPos - cameraPos) / interval * Time.deltaTime;

		moveCamera(delta);
	}

	private void moveCamera(Vector2 delta) {
		
		Vector2 before_translate_dir = delta.normalized, 
		        new_pos = transform.position + new Vector3(delta.x, delta.y, 0f),
				playerPos = new Vector2(currentPlayerTransform.position.x, currentPlayerTransform.position.y),
		        after_translate_dir = (playerPos - new_pos).normalized;

		new_pos = Vector2.Dot(before_translate_dir, after_translate_dir) >= 0 ?  new_pos :
		                                                                         playerPos;

		transform.position = new Vector3(new_pos.x, new_pos.y, transform.position.z);
	}

	public CameraFollow getInstance() {
		if(_instance == null)
			_instance = new CameraFollow();
		return _instance;
	}

	private void updatePlayer() {
		currentPlayerTransform = players[0].beingControlled ? players[0].transform : players[1].transform;
	}
}
