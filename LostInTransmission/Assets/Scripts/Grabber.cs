using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    public LayerMask grabbablesLayer;
    private GameObject grabbed;
    private bool grabbing = false;
    private Transform sceneParent;
    private bool newInput = true;
    public float offsetX = -.5f;
    public float offsetY = .5f;
	// Use this for initialization
	void Start () {
        sceneParent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if (grabbing && grabbed.transform.parent != transform) {
            grabbing = false;
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (Input.GetKeyDown(KeyCode.X))
		{
			if (grabbablesLayer == (grabbablesLayer | (1 << collision.gameObject.layer)))
			{
				//if(grabbing&&grabbed!=collision.gameObject) {
				//    grabbed.transform.parent = sceneParent;
				//}
				if(grabbing) {
	                grabbed.transform.parent = sceneParent;
	            }
                collision.gameObject.transform.position = transform.position + new Vector3(offsetX * transform.localScale.x, offsetY, 0);
				collision.gameObject.transform.parent = transform;
                grabbed = collision.gameObject;
                grabbing = true;
			}
            if (collision.CompareTag("Lever"))
            {
                collision.GetComponent<Lever>().flipLever();
            }
        }
        
    }
}
