using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealByPlayer : MonoBehaviour {

    // Use this for initialization
    TextMeshColorLerp colorLerp;
    private bool dontRevealAgain = false;
	void Start () {
        colorLerp = transform.parent.gameObject.GetComponent<TextMeshColorLerp>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dontRevealAgain)
        {
            colorLerp.startColorChange(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colorLerp.startColorChange(-1);
            dontRevealAgain = true;
        }
    }
}
