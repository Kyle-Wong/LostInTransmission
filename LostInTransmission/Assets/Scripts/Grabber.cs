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
    public AudioClip grabThing;
    public AudioClip flipSwitch;
    private AudioSource source;
    public enum OnObject
    {
        Player, Terminal
    }
    public OnObject thisObject = OnObject.Player;
	// Use this for initialization
	void Start () {
        sceneParent = transform.parent;
        source = GetComponent<AudioSource>();
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
                switch (thisObject)
                {
                    case (OnObject.Player):
                        if (transform.GetComponent<PlayerScript>().beingControlled)
                        {
                            if (grabbing)
                            {
                                grabbed.transform.parent = sceneParent;
                            }
                            collision.gameObject.transform.position = transform.position + new Vector3(offsetX * transform.localScale.x, offsetY, 0);
                            collision.gameObject.transform.parent = transform;
                            grabbed = collision.gameObject;
                            grabbing = true;
                            source.PlayOneShot(grabThing);
                        }
                        else
                        {
                            try
                            {
                                if (collision.transform.parent.gameObject.CompareTag("Player"))
                                {
                                    if (grabbing)
                                    {
                                        grabbed.transform.parent = sceneParent;
                                    }
                                    collision.gameObject.transform.position = transform.position + new Vector3(offsetX * transform.localScale.x, offsetY, 0);
                                    collision.gameObject.transform.parent = transform;
                                    grabbed = collision.gameObject;
                                    grabbing = true;
                                    source.PlayOneShot(grabThing);
                                }
                            }catch
                            {
                                
                            }
                        }
                        break;
                    case (OnObject.Terminal):
                        if (collision.transform.parent.gameObject.GetComponent<PlayerScript>() != null &&
                            collision.transform.parent.gameObject.GetComponent<PlayerScript>().beingControlled)
                        {
                            if (grabbing)
                            {
                                grabbed.transform.parent = sceneParent;
                            }
                            collision.gameObject.transform.position = transform.position + new Vector3(offsetX * transform.localScale.x, offsetY, 0);
                            collision.gameObject.transform.parent = transform;
                            grabbed = collision.gameObject;
                            grabbing = true;
                        }
                        break;
                }
				
			}
            if (collision.CompareTag("Lever"))
            {
                switch (thisObject)
                {
                    case (OnObject.Player):
                        if (GetComponent<PlayerScript>().beingControlled)
                        {
                            collision.GetComponent<Lever>().flipLever();
                            source.PlayOneShot(flipSwitch);
                        }
                        break;
                    case (OnObject.Terminal):
                        //terminals can't pull levers
                        break;
                }
            }
        }
        
    }
}
