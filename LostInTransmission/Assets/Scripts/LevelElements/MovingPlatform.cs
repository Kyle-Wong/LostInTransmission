using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    // Use this for initialization
    public enum PauseAt
    {
        Ends, All, None
    }
    
    public bool linkStateToObject = false;
    public State linkedState;
    public int ACTIVE_STATE = 1;
    public int INACTIVE_STATE = 0;
    private List<Transform> occupants;
    public GameObject[] nodeObjects;
    private Vector3[] nodes;
    public PauseAt pauseType;
    public float pauseDuration;
    public int startNode;
    public float moveSpeed;
    public int direction;               //1 or -1
    public bool isMoving;
    private int destinationNode;
	void Awake () {
        nodes = new Vector3[nodeObjects.Length];
        for(int i = 0; i < nodeObjects.Length; i++)
        {
            nodes[i] = nodeObjects[i].transform.position;
        }
        transform.position = nodes[startNode];
        occupants = new List<Transform>();
        updateNodes();
	}
	
	// Update is called once per frame
	void Update () {
        if (linkStateToObject)
        {
            if(linkedState.getState() == ACTIVE_STATE)
            {
                if (isMoving)
                {
                    movePlatform();
                }
            }
        }
        else
        {
            if (isMoving)
            {
                movePlatform();
            }
        }
	}
    private void updateNodes()
    {
        if(destinationNode <= 0 && direction == -1)
        {
            direction = 1;
            destinationNode = 1;

        }
        else if(destinationNode >= nodes.Length-1 && direction == 1)
        {
            direction = -1;
            destinationNode = nodes.Length - 2;
        } else
        {
            destinationNode = destinationNode + direction;
            
        }
    }
    
    private void movePlatform()
    {

        Vector3 distance = nodes[destinationNode] - transform.position;
        Vector3 moveVector = distance.normalized * moveSpeed * Time.deltaTime;
        transform.position += moveVector;
        if(distance.magnitude < moveSpeed*Time.deltaTime)
        {
            StartCoroutine(pause(pauseDuration));
            updateNodes();
        }
        moveAllOccupants(moveVector);
    }
    private IEnumerator pause(float duration)
    {
        switch (pauseType)
        {
            case (PauseAt.All):
                isMoving = false;
                yield return new WaitForSeconds(duration);
                isMoving = true;
                break;
            case (PauseAt.Ends):
                if(destinationNode == 0 || destinationNode == nodes.Length - 1)
                {
                    isMoving = false;
                    yield return new WaitForSeconds(duration);
                    isMoving = true;
                }
                break;
            case (PauseAt.None):
                break;
        }
    }
    private void moveAllOccupants(Vector3 vec)
    {
        for(int i = 0; i < occupants.Count; i++)
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
