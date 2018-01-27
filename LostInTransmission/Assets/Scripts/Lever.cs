using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    // Use this for initialization
    private State state;
    private const int ACTIVE = 1;
    private const int INACTIVE = -1;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    SpriteRenderer spriteRenderer;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void flipLever()
    {
        if(state.getState() == ACTIVE)
        {
            state.setState(INACTIVE);
            spriteRenderer.sprite = inactiveSprite;
        } else
        {
            state.setState(ACTIVE);
            spriteRenderer.sprite = activeSprite;
        }
    }
}
