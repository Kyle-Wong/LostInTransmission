using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    // Use this for initialization
    public bool linkStateToObject;
    public State linkedState;
    public int openState = 1;
    public int closedState = 0;
    private State state;
    private BoxCollider2D col;
    private ColorLerp colorLerp;
    public bool useColorLerp = false;
    Animator anim;
	void Start () {
        state = GetComponent<State>();
        col = GetComponent<BoxCollider2D>();
        colorLerp = GetComponent<ColorLerp>();
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (linkStateToObject)
        {
            if (linkedState.getState() == openState && state.getState() == closedState)
            {
                openDoor();
            }
            else if (linkedState.getState() == closedState && state.getState() == openState)
            {
                closeDoor();
            }
        }
    }
    public void openDoor()
    {
        col.enabled = false;
        state.setState(openState);
        if(useColorLerp)
            colorLerp.startColorChange(1);
        anim.SetBool("Is_Open", true);
    }
    public void closeDoor()
    {
        col.enabled = true;
        state.setState(closedState);
        if(useColorLerp)
            colorLerp.startColorChange(-1);
        anim.SetBool("Is_Open", false);

    }
}
