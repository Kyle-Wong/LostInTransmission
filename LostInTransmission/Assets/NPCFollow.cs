using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour {

    // Use this for initialization
    [HideInInspector]
    public bool followingPlayer;
    private PlayerScript player;
    public float followDistance;
    private bool flipped;
    public GameObject spriteObject;
    private SpriteRenderer spriteRenderer;
    private Vector3 destination;
    public float moveSpeed;
    private Vector3 posLastFrame;
    public float teleportDistance;
    public Sprite happySprite;
    public Sprite sadSprite;
    public float yOffset;
	void Start () {
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (followingPlayer)
        {
            
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            destination = player.transform.position + Vector3.left * player.getDirection() * followDistance;
            if (Vector3.Distance(destination, transform.position) > teleportDistance)
            {
                transform.position = destination;
            }
            Vector3 moveVector;
            if(Vector3.Distance(destination,transform.position) < moveSpeed * Time.deltaTime)
            {
                moveVector = destination - transform.position;
                transform.position = destination;
            } else
            {
                moveVector = (destination - transform.position).normalized * moveSpeed * Time.deltaTime;
                transform.position += moveVector;
            }
            
            spriteRenderer.flipX = (player.getDirection() == 1);
            spriteObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0,  moveVector.x*50* Mathf.Cos(Time.time * 15+Mathf.PI));
            spriteRenderer.sprite = happySprite;
        } else
        {
            spriteRenderer.sprite = sadSprite;
        }
    }
    
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player")
        {
            gameObject.layer = LayerMask.NameToLayer("NPC");
            followingPlayer = true;
            player = coll.gameObject.GetComponent<PlayerScript>();
        }
	}
}
