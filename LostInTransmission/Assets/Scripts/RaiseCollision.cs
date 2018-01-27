using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseCollision : MonoBehaviour {

    // Use this for initialization
    public bool onEnter;
    public bool onStay;
    public bool onExit;
    public GameObject target;
    public LayerMask layerToDetect;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onEnter && layerToDetect == (layerToDetect | (1 << collision.gameObject.layer)))
            target.SendMessage("OnChildTriggerEnter2D", collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onStay && layerToDetect == (layerToDetect | (1 << collision.gameObject.layer)))
            target.SendMessage("OnChildTriggerStay2D", collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onExit && layerToDetect == (layerToDetect | (1 << collision.gameObject.layer)))
            target.SendMessage("OnChildTriggerExit2D", collision);
    }
}
