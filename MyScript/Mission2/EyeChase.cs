using UnityEngine;
using System.Collections;

public class EyeChase : MonoBehaviour {
	public AudioSource audio;
	public float radius =2.0f;
	public float speed = 80.0f;
	public float mass = 5.0f;
	public float randomSphereRadius = 100.0f;
	public bool isLooping = true;
	public GameObject randomSphere;
	public GameObject door;
	public GameObject particle;
	//Actual speed of the vehicle 
	private float curSpeed;
	private Vector3 targetPoint;
	private Vector3 nextPoint;
	
	Vector3 velocity;
	
	// Use this for initialization
	void Start () 
	{
		nextPoint = randomSphere.transform.position + (Random.onUnitSphere * randomSphereRadius);
		//get the current velocity of the vehicle
		velocity = transform.forward;
	}
	Vector3 PointGeneration(){
		return randomSphere.transform.position + (Random.onUnitSphere * randomSphereRadius);
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("Player")) {
			door.SendMessage("PlayerWin");
			audio.Play();
			Instantiate(particle,transform.position,Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		//Unify the speed
		curSpeed = speed * Time.deltaTime;
		
		targetPoint = nextPoint;
		
		//If reach the radius within the path then move to next point in the path
		if(Vector3.Distance(transform.position, targetPoint) < radius)
		{
			nextPoint= PointGeneration();
		}
		

		velocity += Steer(targetPoint);
		
		transform.position += velocity; //Move the vehicle according to the velocity
		transform.rotation = Quaternion.LookRotation(velocity); //Rotate the vehicle towards the desired Velocity
	}
	
	//Steering algorithm to steer the vector towards the target
	public Vector3 Steer(Vector3 target)
	{
		//Calculate the directional vector from the current position towards the target point
		Vector3 desiredVelocity = (target - transform.position);
		float dist = desiredVelocity.magnitude;
		
		//Normalise the desired Velocity
		desiredVelocity.Normalize();

		desiredVelocity *= curSpeed;
		
		//Calculate the force Vector
		Vector3 steeringForce = desiredVelocity - velocity; 
		Vector3 acceleration = steeringForce / mass;
		
		return acceleration;
	}
}
