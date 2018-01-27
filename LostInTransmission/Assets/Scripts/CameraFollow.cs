using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public Transform currentPlayerTransform;
	private static CameraFollow _instance;
	private bool isFollowing = true;

	void Awake() {
		Debug.Assert(currentPlayerTransform);
	}

	void Start() {
		Debug.Assert(GameObject.FindWithTag("NewPlayer"));
		followNewPlayer(GameObject.FindWithTag("NewPlayer"), 1f);

	}
	
	private float interval = 1f;
	// Update is called once per frame
	void Update () {
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

	public void followNewPlayer(GameObject player, float speed) {
		currentPlayerTransform = player.transform;
		interval = 1 / speed;
	}

	// private IEnumerator movePlayer(Transform nextPlayerTransform, Vector2 delta, int intervals) {
	// 	isFollowing = false;
	// 	for(int i = 0; i < intervals; ++i) {
	// 		transform.position += new Vector3(delta.x, delta.y, 0f) * Time.deltaTime;
	// 		yield return null;
	// 	}
	// 	isFollowing = true;
	// 	currentPlayerTransform = nextPlayerTransform;
	// }
}
