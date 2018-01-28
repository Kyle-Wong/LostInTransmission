﻿﻿using System.Collections;
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
    public LayerMask grabbablesLayer;
    private bool flipped = false;
    private GameObject sprite;
    private Vector2 size;
    private GameObject indicator;
    public Transform soulPrefab;
    private GameObject otherPlayer;
    public float switchDelay;
    private float switchTimer;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpForce = jumpForce * rigidBody.gravityScale;
        sprite = transform.Find("Sprite").gameObject;
        indicator = transform.Find("Indicator").gameObject;
        size = transform.localScale;
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
            if(g.name != gameObject.name)
            {
                otherPlayer = g;
                break;
            }
        }
        switchTimer = 0;
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
        if (Input.GetKeyDown(KeyCode.Z) && switchTimer <= 0) {
            switchTimer = switchDelay;
            if (beingControlled) {
                spawnSoul();
            }
            beingControlled = !beingControlled;
        }
        if(switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
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
            transform.localScale = new Vector2(1.05f * this.size.x, .75f*this.size.y);
            crouching = true;
        } else {
            transform.localScale = this.size;
            crouching = false;
        }
        if (!flipped) {
            transform.localScale -= transform.localScale.x * Vector3.right * 2;
        }
        inputY = vertical;
        vx = horizontal * speed;
        if (crouching && grounded) {
            vx = 0;
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
        sprite.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rigidBody.velocity.x * Mathf.Cos(Time.time*15));
        indicator.SetActive(beingControlled);
        if(rigidBody.velocity.y<-rigidBody.gravityScale||rigidBody.velocity.y>1) {
            grounded = false;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if(rigidBody.velocity.y>=-1)
		grounded = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (grabbablesLayer == (grabbablesLayer | (1 << collision.gameObject.layer)))
            {
                collision.gameObject.transform.position = transform.position + new Vector3(-.5f * transform.localScale.x, .5f, 0);
                collision.gameObject.transform.parent = transform;
            } else if (collision.CompareTag("Lever"))
            {
                collision.GetComponent<Lever>().flipLever();
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    public int getDirection()
    {
        return (flipped) ? -1 : 1;
    }
    public Vector2 getVel()
    {
        return rigidBody.velocity;
    }
    private void spawnSoul()
    {
        GameObject soul = Instantiate(soulPrefab, transform.position, transform.rotation).gameObject;
        soul.GetComponent<MoveToPoint>().setTarget(otherPlayer.transform);
    }
}
