using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HittableButton : MonoBehaviour {

    // Use this for initialization
    public bool timedActivation = false;
    public float activeDuration = 0;            //only used if this is a timer button
    private float activeTimer = 0;
    public bool flipFlopOnHit = false;          //switches between 0 and 1 each hit
    public LayerMask hitByLayer;
    private const int ACTIVE = 1;
    private const int INACTIVE = 0;
    private State state;
    private SpriteRenderer spriteRenderer;
    public Color activeColor;
    public Color inactiveColor;
    private int currentCollisions = 0;
	void Start () {
        state = GetComponent<State>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (state.getState())
        {
            case (ACTIVE):
                spriteRenderer.color = activeColor;
                break;
            case (INACTIVE):
                spriteRenderer.color = inactiveColor;
                break;
        }
        if (timedActivation && state.getState() == ACTIVE)
        {
            if(activeTimer > 0)
            {
                activeTimer -= Time.deltaTime;
            } else
            {
                deactivateButton();
            }
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(hitByLayer == (hitByLayer | (1 << collision.gameObject.layer)))
        {
            currentCollisions++;
            if(state.getState() == ACTIVE && flipFlopOnHit)
            {
                deactivateButton();
            }
            else if(state.getState() == INACTIVE)
            {
                activateButton();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
		if (hitByLayer == (hitByLayer | (1 << collision.gameObject.layer)))
		{
            currentCollisions--;
			if (state.getState() == ACTIVE && !flipFlopOnHit)
			{
                if(currentCollisions<=0) {
					deactivateButton();
				}
			}
		}
    }
    public void activateButton()
    {
        state.setState(ACTIVE);
        if (timedActivation)
            activeTimer = activeDuration;
    }
    public void deactivateButton()
    {
        state.setState(INACTIVE);
    }
}
