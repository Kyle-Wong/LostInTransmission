using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOnEscape : MonoBehaviour {

    // Use this for initialization
    public bool firstEscape = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && firstEscape)
            {
                firstEscape = false;
            } else if(Input.GetKeyDown(KeyCode.Escape) && !firstEscape)
            {
                Application.Quit();
            } else
            {
                firstEscape = true;
            }
        }
	}
}
