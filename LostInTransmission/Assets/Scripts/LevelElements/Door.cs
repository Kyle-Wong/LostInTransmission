﻿using System.Collections;
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

	void Start () {
        state = GetComponent<State>();
        col = GetComponent<BoxCollider2D>();
        colorLerp = GetComponent<ColorLerp>();
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
        colorLerp.startColorChange(1);
    }
    public void closeDoor()
    {
        col.enabled = true;
        state.setState(closedState);
        colorLerp.startColorChange(-1);

    }
}
