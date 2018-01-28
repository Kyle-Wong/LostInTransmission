using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPrompt : MonoBehaviour {

    // Use this for initialization
    public ReversibleColorLerp colorLerp;
    public ColorLerp activationLerp;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colorLerp.startColorChange(1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            activationLerp.startColorChange(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colorLerp.startColorChange(-1);

        }
    }
}
