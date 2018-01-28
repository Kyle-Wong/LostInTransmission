﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	//NOTE: you will need to query Kyle's delay
	// Use this for initialization
	private Transform currentPlayerTransform;
	private PlayerScript [] players = new PlayerScript[2];
	public float speed;
	private float interval;
	private static CameraFollow _instance;
	private static bool isFollowing = true;
    public Vector3 offset;
    public Vector2 min = new Vector2(-1000,-1000);
    public Vector2 max = new Vector2(1000,1000);
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
        Vector2 playerPos = currentPlayerTransform.position + offset;
		Vector2 cameraPos = transform.position;

		Vector2 delta = (playerPos - cameraPos) / interval * Time.deltaTime;

		moveCamera(delta);
	}

	private void moveCamera(Vector2 delta) {
		
		Vector2 before_translate_dir = delta.normalized, 
		        new_pos = transform.position + new Vector3(delta.x, delta.y, 0f),
        playerPos = new Vector2(currentPlayerTransform.position.x + offset.x, currentPlayerTransform.position.y + offset.y),
		        after_translate_dir = (playerPos - new_pos).normalized;

		new_pos = Vector2.Dot(before_translate_dir, after_translate_dir) >= 0 ?  new_pos :
		                                                                         playerPos;
        new_pos = ClampPos(new_pos);
		transform.position = new Vector3(new_pos.x, new_pos.y, transform.position.z);
	}

    private Vector2 ClampPos(Vector2 new_pos) {
		float verticalSize = Camera.main.orthographicSize * 2.0f;
		float horizontalSize = verticalSize * Screen.width / Screen.height;
		new_pos.x = Mathf.Clamp(new_pos.x, min.x + horizontalSize / 2, max.x - horizontalSize / 2);
		new_pos.y = Mathf.Clamp(new_pos.y, min.y + verticalSize / 2, max.y - verticalSize / 2);
        return new_pos;
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
