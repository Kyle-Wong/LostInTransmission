using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {


    public State linkedState;
 
    public int ACTIVE_STATE = 1;
    public int INACTIVE_STATE = 0;
    private List<Transform> occupants;
    public Transform startNode;
    public Transform endNode;
    public float moveSpeed;
    void Awake()
    {

        transform.position = startNode.position;
        occupants = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (linkedState.getState() == ACTIVE_STATE)
            {
                movePlatform(endNode.position);
            } else
            {
                movePlatform(startNode.position);
            }
        
        
    }
    

    private void movePlatform(Vector3 node)
    {

        Vector3 distance = node - transform.position;
        Vector3 moveVector = distance.normalized * moveSpeed * Time.deltaTime;
        if (distance.magnitude < moveSpeed * Time.deltaTime)
        {
            moveAllOccupants(distance.normalized*distance.magnitude);
            transform.position = node;

        } else
        {
            transform.position += moveVector;
            moveAllOccupants(moveVector);

        }
    }
    
    private void moveAllOccupants(Vector3 vec)
    {
        for (int i = 0; i < occupants.Count; i++)
        {
            occupants[i].position += vec;
        }
    }
    private void OnChildTriggerEnter2D(Collider2D collision)
    {
        if (!occupants.Contains(collision.transform))
        {
            occupants.Add(collision.transform);
        }
    }
    private void OnChildTriggerExit2D(Collider2D collision)
    {
        occupants.Remove(collision.transform);
    }
}


