using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    // Use this for initialization
    private State state;
    private const int ACTIVE = 1;
    private const int INACTIVE = 0;

    //animator

    Animator anim;
	void Start () {
        state = GetComponent<State>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void flipLever()
    {
        if(state.getState() == ACTIVE)
        {
            anim.SetBool("Is_Open", false);
            state.setState(INACTIVE);
           
        } else
        {
            anim.SetBool("Is_Open", true);
            state.setState(ACTIVE);
            
        }
    }
}
