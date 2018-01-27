using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private float jumpForce = 4f;
    private Rigidbody2D rigidBody;
    private float vx = 0;
    private float speed = 10f;
    private bool grounded = false;
    private bool crouching = false;
    private float inputY = 0;
    public bool beingControlled = true;
    public bool flipped = false;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpForce = jumpForce * rigidBody.gravityScale;
	}
	// Update is called once per frame
	void Update () {
        float horizontal = 0;
        float vertical = 0;
        bool jumpDown = false;
        bool jumpUp = false;
        if (beingControlled) {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            jumpDown = Input.GetButtonDown("Jump");
            jumpUp = Input.GetButtonUp("Jump");
        }
        if (Mathf.Abs(horizontal) > .2f) {
            flipped = horizontal < 0;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            beingControlled = !beingControlled;
        }
        if (jumpDown && grounded) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }
        if (jumpUp && rigidBody.velocity.y > 0) {
            rigidBody.velocity += (-rigidBody.velocity.y / 2) * Vector2.up;
        }
        if (vertical < -.8f) {
            if (rigidBody.velocity.y < 0 && inputY > -.8f) {
                rigidBody.velocity += Vector2.down * 6;
            }
            transform.localScale = new Vector2(1.05f, .75f);
            crouching = true;
        } else {
            transform.localScale = new Vector2(1f, 1f);
            crouching = false;
        }
        if (!flipped) {
            transform.localScale -= transform.localScale.x * Vector3.right * 2;
        }
        inputY = vertical;
        vx = horizontal * speed;
        if (crouching && grounded) {
			rigidBody.velocity += (-rigidBody.velocity.x) * Vector2.right / 10;
		} else {
            rigidBody.velocity += (vx - rigidBody.velocity.x) * Vector2.right / 3;
        }
        //if(!grounded) {
        //    float direction = -1;
        //    if (flipped) direction = 1;
        //    transform.Rotate(Vector3.forward * 10 * direction);
        //} else {
        //    transform.rotation = Quaternion.identity;
        //}
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        grounded = true;
    }
}
