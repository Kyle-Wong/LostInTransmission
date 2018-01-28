using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    // Use this for initialization
    public State linkedState;
    private State myState;
    public enum Direction
    {
       CW, CounterCW
    }
    public Direction direction;
    public float rotMagnitude = 90;
    private int rotDirection;
    public const int ACTIVE = 1;
    public const int INACTIVE = 0;
    public float rotSpeed;
    private Vector3 activeAngle;
    private Vector3 inactiveAngle;
	void Start () {
        
        myState = GetComponent<State>();
        rotDirection = (direction == Direction.CW) ? 1 : -1;
        inactiveAngle = transform.rotation.eulerAngles;
        activeAngle = inactiveAngle + new Vector3(0, 0, rotMagnitude * rotDirection);
	}
	
	// Update is called once per frame
	void Update () {
        myState.setState(linkedState.getState());
		if(myState.getState() == ACTIVE)
        {
            rotateToDirection(activeAngle);
        }
        else
        {
            rotateToDirection(inactiveAngle);
        }
	}
    private void rotateToDirection(Vector3 desiredAngle)
    {
        float angleBetween = Quaternion.Angle(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(desiredAngle));

        if(angleBetween < rotSpeed * Time.deltaTime)
        {
            transform.rotation = Quaternion.Euler(desiredAngle);
        } else
        {
            transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(desiredAngle),rotSpeed*Time.deltaTime);
        }
    }
}
