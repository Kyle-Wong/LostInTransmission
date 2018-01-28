using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioObject : MonoBehaviour {

    private Transform sceneParent;
    private GameObject waveEffect;
    private Vector2 maxSize;
    private Vector2 size;
    public bool forceOn = false;
    public AudioClip sound1;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        sceneParent = transform.parent;
        waveEffect = transform.Find("waveEffect").gameObject;
        maxSize = waveEffect.transform.localScale;
        size = new Vector2(1, 1);
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (forceOn)
        {
            waveEffect.transform.localScale = maxSize * 2;
            source.Play();
        }
        else
        {
            if (transform.parent == sceneParent)
            {
                size += (new Vector2(0, 0) - size) / 10;
                source.Stop();
            }
            else
            {
                size += (new Vector2(maxSize.x, maxSize.y) - size) / 10;
                source.Play();
            }
            waveEffect.transform.localScale = size;
        }
	}
    
}
