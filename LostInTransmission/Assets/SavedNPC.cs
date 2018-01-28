using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedNPC : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer spriteRenderer;
    public int savedInLevelNumber;
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(GameData.npcsSaved[savedInLevelNumber] == 1)
        {
            spriteRenderer.enabled = true;
        } else
        {
            spriteRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
