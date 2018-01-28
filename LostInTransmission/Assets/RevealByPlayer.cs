using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealByPlayer : MonoBehaviour {

    // Use this for initialization
    TextMeshColorLerp colorLerp;
    private bool revealText = false;
    private bool dontRevealAgain = false;
    private float stayDuration = 3f;
    private float stayTimer;
	void Start () {
        colorLerp = transform.parent.gameObject.GetComponent<TextMeshColorLerp>();
        stayTimer = stayDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (stayTimer > 0 && revealText)
        {
            stayTimer -= Time.deltaTime;
            colorLerp.startColorChange(1);

        } else if(stayTimer <= 0 && revealText)
        {
            revealText = false;
            colorLerp.startColorChange(-1);
            dontRevealAgain = true;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dontRevealAgain)
        {
            revealText = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dontRevealAgain = true;
        }
    }
}
