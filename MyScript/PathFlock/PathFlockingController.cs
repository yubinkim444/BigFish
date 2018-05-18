using UnityEngine;
using System.Collections;

public class PathFlockingController : MonoBehaviour {

	public FlockPath path;
	public float speed = 20.0f;
	public float mass = 5.0f;
	public bool isLooping = true;
	
	//Actual speed of the vehicle 
	private float deltaDist;
	
	private int curPathIndex;
	private float pathLength;
	private Vector3 targetPoint;
	
	Vector3 curDeltaDiplacement;
	
	// Use this for initialization
	void Start () 
	{
		pathLength = path.Length;
		curPathIndex = 0;
		
		//get the current velocity of the vehicle
		curDeltaDiplacement = transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Unify the speed
		deltaDist = speed * Time.deltaTime;
		
		targetPoint = path.GetPoint(curPathIndex);
		
		//If reach the radius within the path then move to next point in the path
		if(Vector3.Distance(transform.position, targetPoint) < path.Radius)
		{
			//Don't move the vehicle if path is finished 
			if (curPathIndex < pathLength - 1)
				curPathIndex ++;
			else if (isLooping)
				curPathIndex = 0;
			else
				return;
		}

		
		//Calculate the next Velocity towards the path
		if(curPathIndex == pathLength - 1 && !isLooping)
			curDeltaDiplacement += Steer(targetPoint, true)* (Time.deltaTime * Time.deltaTime);
		else
			curDeltaDiplacement += Steer(targetPoint)* (Time.deltaTime * Time.deltaTime);
		
		transform.position += curDeltaDiplacement; //Move the vehicle according to the velocity
		transform.rotation = Quaternion.LookRotation(curDeltaDiplacement); //Rotate the vehicle towards the desired Velocity
	}
	
	//Steering algorithm to steer the vector towards the target
	public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
	{
		//Calculate the directional vector from the current position towards the target point
		Vector3 desiredPosDirection = (target - transform.position);
		float dist = desiredPosDirection.magnitude;
		Vector3 desireDiplacement;
		//Normalise the desired Velocity
		desiredPosDirection.Normalize();
		
		//Calculate the velocity according to the speed
		if (bFinalPoint && dist < 10.0f)
			desireDiplacement = (deltaDist * (dist / 10.0f)) * desiredPosDirection;
		else 
			desireDiplacement = deltaDist * desiredPosDirection;
		float k = 1.0f / (Time.deltaTime * Time.deltaTime);
		//Calculate the force Vector
		Vector3 steeringForce = k* (desireDiplacement-curDeltaDiplacement); 
		Vector3 acceleration = steeringForce / mass;
		
		return acceleration;
	}
}
