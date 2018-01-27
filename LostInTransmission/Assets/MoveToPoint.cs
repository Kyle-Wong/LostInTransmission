using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour {

    // Use this for initialization
    public bool deleteAtDestination;
    public float moveSpeed;
    public Transform target;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}
    private void move()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < moveSpeed * Time.deltaTime)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position += (target.position - transform.position) * moveSpeed * Time.deltaTime;
            }
        }
    }
    public void setTarget(Transform t)
    {
        target = t;
    }
}
