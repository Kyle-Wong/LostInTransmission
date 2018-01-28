﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealByPlayer : MonoBehaviour {

    // Use this for initialization
    TextMeshColorLerp colorLerp;
    private bool canFadeText = false;
    private float stayDuration = 3f;
    private List<GameObject> playerList;
	void Start () {
        playerList = new List<GameObject>();
        colorLerp = transform.parent.gameObject.GetComponent<TextMeshColorLerp>();
	}
	
	// Update is called once per frame
	void Update () {
        print(canFadeText);
        if (playerList.Count <= 0 && canFadeText)
        {
            colorLerp.startColorChange(-1);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerList.Contains(collision.gameObject))
            {
                playerList.Add(collision.gameObject);
                StartCoroutine(doText());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerList.Remove(collision.gameObject);
            
        }
    }
    private IEnumerator doText()
    {
        colorLerp.startColorChange(1);
        yield return new WaitForSeconds(stayDuration);
        canFadeText = true;
    }
}
