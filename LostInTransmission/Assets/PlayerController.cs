using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    private Prime31.CharacterController2D charController;
    private float xVel, yVel;
    public float moveSpeed;
    public float jumpVel;
    public float gravity;
    private bool onGround;
	void Start () {
        charController = GetComponent<Prime31.CharacterController2D>();
        charController.warpToGrounded();
        print(charController.isGrounded);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        updateYVelocity();
        updateXVelocity();
        print(charController.collisionState);

    }
    private void moveHorizontal(float xV)
    {
        charController.move(new Vector3(xV, 0));
    }
    private void moveVertical(float yV)
    {
        charController.move(new Vector3(0, yV));
    }
    private void updateXVelocity()
    {
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            xVel -= moveSpeed * Time.deltaTime;
        } else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            xVel += moveSpeed * Time.deltaTime;
        } else
        {
            xVel *= .8f;
        }
        moveHorizontal(xVel);
    }
    private void updateYVelocity()
    {
        if (!charController.isGrounded)
        {
            yVel -= gravity * Time.deltaTime;
        } else
        {
            yVel = 0f;
        }
        moveVertical(yVel);
    }
}
