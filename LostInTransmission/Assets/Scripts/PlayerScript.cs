﻿﻿﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float jumpForce = 4f;
    private Rigidbody2D rigidBody;
    private float vx = 0;
    public float speed = 10f;
    private bool grounded = false;
    private bool crouching = false;
    private float inputY = 0;
    public bool beingControlled = true;
    public LayerMask grabbablesLayer;
    public LayerMask groundLayer;
    private bool flipped = false;
    private GameObject sprite;
    private Vector2 size;
    private GameObject indicator;
    public Transform soulPrefab;
    private GameObject otherPlayer;
    public float switchDelay;
    private float switchTimer;
    private Animator animator;
    public AudioClip swapChar;
    private AudioSource source;
	public float maxVY = 1;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpForce = jumpForce * rigidBody.gravityScale;
        sprite = transform.Find("Sprite").gameObject;
        indicator = transform.Find("Indicator").gameObject;
        size = transform.localScale;
        animator = sprite.GetComponent<Animator>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
            if(g.name != gameObject.name)
            {
                otherPlayer = g;
                break;
            }
        }
        switchTimer = 0;
        source = GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
        float horizontal = 0;
        float vertical = 0;
        bool jumpDown = false;
        bool jumpUp = false;
        if(Mathf.Abs(rigidBody.velocity.x)>.2f && grounded)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            source.Stop();
        }
        if (beingControlled) {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            jumpDown = Input.GetButtonDown("Jump");
            jumpUp = Input.GetButtonUp("Jump");
        }
        if (inputY >= .8f && vertical < .8f) {
            jumpUp = true;
        }
		if (vertical >= .8f && inputY < .8f)
		{
			jumpDown = true;
		}
        if (Mathf.Abs(horizontal) > .2f) {
            flipped = horizontal < .1f;
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
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
            animator.SetBool("Jump", true);
        }
        if (jumpUp && rigidBody.velocity.y > 0) {
            rigidBody.velocity += (-rigidBody.velocity.y / 2) * Vector2.up;
        }
        if (vertical < -.8f) {
            if (rigidBody.velocity.y < 0 && inputY > -.8f) {
                rigidBody.velocity += Vector2.down * 6;
            }
            //transform.localScale = new Vector2(1.05f * this.size.x, .75f*this.size.y);
            animator.SetBool("Crouch", true);
            animator.SetBool("Walking", false);
            crouching = true;
        } else {
			//transform.localScale = this.size;
			animator.SetBool("Crouch", false);
			crouching = false;
        }
        transform.localScale = this.size;
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
        //sprite.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rigidBody.velocity.x * Mathf.Cos(Time.time*15));
        indicator.SetActive(beingControlled);
        //if(rigidBody.velocity.y<-rigidBody.gravityScale||rigidBody.velocity.y>1) {
        //    grounded = false;
        //}
        if (!grounded && rigidBody.velocity.y<0) {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
        GroundDetect();
        if (rigidBody.velocity.y < -maxVY) rigidBody.velocity = new Vector2(rigidBody.velocity.x, -maxVY);
	}
    private void GroundDetect() {
		RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, Vector2.down, .5f, groundLayer);

        grounded = Mathf.Abs(hitInformation.distance) > 0;
        if(grounded) {
			animator.SetBool("Jump", false);
			animator.SetBool("Fall", false);
        }

        //hitInformation.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

	//private void OnCollisionStay2D(Collision2D collision)
	//{
 //       if (rigidBody.velocity.y > -1)
	//	{
	//		grounded = true;
	//		animator.SetBool("Fall", false);
	//		animator.SetBool("Jump", false);
	//	}
	//}
    private void OnTriggerStay2D(Collider2D collision)
    {
        
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
        source.PlayOneShot(swapChar);
    }
}
