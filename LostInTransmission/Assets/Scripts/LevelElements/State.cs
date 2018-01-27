using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

    // Use this for initialization
    public int state;


	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int getState()
    {
        return state;
    }
    public void setState(int x)
    {
        state = x;
    }
}
