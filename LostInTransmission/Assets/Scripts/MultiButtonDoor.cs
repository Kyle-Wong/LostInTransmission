using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiButtonDoor : MonoBehaviour
{

    // Use this for initialization
    public enum ActivationMode
    {
        All, Any
    }
    public ActivationMode mode;
    public State[] linkedState;
    public int openState = 1;
    public int closedState = 0;
    private State state;
    private BoxCollider2D col;
    private ColorLerp colorLerp;

    void Start()
    {
        state = GetComponent<State>();
        col = GetComponent<BoxCollider2D>();
        colorLerp = GetComponent<ColorLerp>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case (ActivationMode.All):
                if (allActivated() && state.getState() == closedState)
                {
                    openDoor();
                }
                else if (!allActivated() && state.getState() == openState)
                {
                    closeDoor();
                }
                break;
            case (ActivationMode.Any):
                if (anyActivated() && state.getState() == closedState)
                {
                    openDoor();
                }
                else if (!anyActivated() && state.getState() == openState)
                {
                    closeDoor();
                }
                break;
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
    private bool allActivated()
    {
        for(int i = 0; i < linkedState.Length; i++)
        {
            if (linkedState[i].getState() == closedState)
                return false;
        }
        return true;
    }
    private bool anyActivated()
    {
        for (int i = 0; i < linkedState.Length; i++)
        {
            if (linkedState[i].getState() == openState)
                return true;
        }
        return false;
    }
}

