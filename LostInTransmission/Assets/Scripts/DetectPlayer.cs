using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    // Use this for initialization
    private State state;
    public  int DETECTED = 1;
    public int NOT_DETECTED = 0;
	void Start () {
        state = GetComponent<State>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            state.setState(DETECTED);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            state.setState(DETECTED);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            state.setState(NOT_DETECTED);
    }
}
