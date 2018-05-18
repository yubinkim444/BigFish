using UnityEngine;
using System.Collections;

public class MermaidFlock : MonoBehaviour 
{
	private string[] ani = {"Swim forward","Swim death","Swim backward","Swim (in place)","Swim blink","Swim idle","Swim attack 2","Swim attack 3","Swim cast spell"};
	//avoid obstacle
	public float force = 50.0f;
	public float minimumDistToAvoid = 20.0f;
	//----------------------------------------
	public float minSpeed = 10.0f;         //movement speed of the flock
	public float turnSpeed = 10.0f;         //rotation speed of the flock
	public float randomFreq = 10.0f;        
	
	public float randomForce = 10.0f;       //Force strength in the unit sphere
	public float toOriginForce = 20.0f;     
	public float toOriginRange = 50.0f;
	
	public float gravity = 2.0f;            //Gravity of the flock
	
	public float avoidanceRadius = 20.0f;  //Minimum distance between flocks
	public float avoidanceForce = 10.0f;
	
	public float followVelocity = 4.0f;
	public float followRadius = 5.0f; 
	public float playerFollowTime = 30.0f;
	//Minimum Follow distance to the leader
	//for change origin
	private GameObject player;
	private Transform fishPoint;
	private Transform pathFollower;
	private Vector3 origin; 
	//Parent transform
	private Vector3 velocity;               //Velocity of the flock
	private Vector3 normalizedVelocity;
	private Vector3 randomPush;             //Random push value
	private Vector3 originPush;
	private Transform[] objects;            //Flock objects in the group
	private MermaidFlock[] otherFlocks;       //Unity Flocks in the group
	private Transform transformComponent;   //My transform
	private float followingTimer;
	
	
	private float speed;
	private Vector3 avgVelocity;
	private Vector3 avgPosition;
	private float count;
	private float f;
	private float d;
	private Vector3 myPosition;
	private Vector3 forceV;
	private Vector3 toAvg;
	private Vector3 wantedVel;
	
	
	void Start ()
	{
		randomFreq = 1.0f / randomFreq;
		
		//Assign the parent as origin
		origin = transform.parent.position;  
		player = GameObject.Find ("First Person Controller");
		fishPoint = GameObject.Find ("FishPoint").transform;
		pathFollower = transform.parent;
		//Flock transform           
		transformComponent = transform;
		
		//Temporary components
		Component[] tempFlocks= null;
		
		//Get all the unity flock components from the parent transform in the group
		if (transform.parent)
		{
			tempFlocks = transform.parent.GetComponentsInChildren<MermaidFlock>();
		}
		
		//Assign and store all the flock objects in this group
		objects = new Transform[tempFlocks.Length];
		otherFlocks = new MermaidFlock[tempFlocks.Length];
		
		for(int i = 0;i<tempFlocks.Length;i++)
		{
			objects[i] = tempFlocks[i].transform;
			otherFlocks[i] = (MermaidFlock)tempFlocks[i];
		}
		
		//Null Parent as the flock leader will be UnityFlockController object
		transform.parent = null;
		
		//Calculate random push depends on the random frequency provided
		StartCoroutine(UpdateRandom());
		StartCoroutine(Ani());
	}
	
	IEnumerator UpdateRandom ()
	{
		while(true)
		{
			randomPush = Random.insideUnitSphere * randomForce;
			yield return new WaitForSeconds(randomFreq + Random.Range(-randomFreq / 2.0f, randomFreq / 2.0f));
		}
	}
	IEnumerator Ani(){
		while (true) {
						animation.Play (ani [Random.Range (0, 9)]);
						yield return new WaitForSeconds (3.0f);
				}
	}
	public void AvoidanceAndFollowPlayer(){
		
		Vector3 otherPosition = player.transform.position;			
		// Average position to calculate cohesion
		avgPosition += otherPosition;			
		//Directional vector from other flock to this flock
		forceV = myPosition - otherPosition;
		
		//Magnitude of that directional vector(Length)
		d = forceV.magnitude;
		if (d < 80 && followingTimer <= playerFollowTime) {
			followingTimer += Time.deltaTime;
			origin =  fishPoint.position;
			count++;
			//Add push value if the magnitude is less than follow radius to the leader
			if (d < followRadius) {
				//calculate the velocity based on the avoidance distance between flocks 
				//if the current magnitude is less than the specified avoidance radius
				if (d < avoidanceRadius) {
					f = 1.0f - (d / avoidanceRadius);
					if (d > 0){
						avgVelocity += (forceV / d) * f * avoidanceForce;
					}
				}
				
			}
		} 
		else {
			origin = pathFollower.transform.position;		
		}
	}
	//Calculate the new directional vector to avoid the obstacle
	public void AvoidObstacles(ref Vector3 dir)
	{
		RaycastHit hit;
		
		//Only detect layer 8 (Obstacles)
		int layerMask = 1 << 8;
		
		//Check that the vehicle hit with the obstacles within it's minimum distance to avoid
		if (Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask))
		{
			//Get the normal of the hit point to calculate the new direction
			Vector3 hitNormal = hit.normal;
			//hitNormal.y = 0.0f; //Don't want to move in Y-Space
			
			//Get the new directional vector by adding force to vehicle's current forward vector
			dir = transform.forward + hitNormal * force;
		}
		
	}
	void Update ()
	{ 
		//Internal variables
		speed= velocity.magnitude;
		avgVelocity = Vector3.zero;
		avgPosition = Vector3.zero;
		count = 0;
		f = 0.0f;
		d = 0.0f;
		myPosition = transformComponent.position;
		forceV = Vector3.zero;
		toAvg =Vector3.zero;
		wantedVel =Vector3.zero;
		
		for(int i = 0;i<objects.Length;i++)
		{
			Transform transform= objects[i];
			
			if (transform != transformComponent)
			{
				Vector3 otherPosition = transform.position;
				
				// Average position to calculate cohesion
				avgPosition += otherPosition;
				count++;
				
				//Directional vector from other flock to this flock
				forceV = myPosition - otherPosition;
				
				//Magnitude of that directional vector(Length)
				d= forceV.magnitude;
				
				//Add push value if the magnitude is less than follow radius to the leader
				if (d < followRadius)
				{
					//calculate the velocity based on the avoidance distance between flocks 
					//if the current magnitude is less than the specified avoidance radius
					if(d < avoidanceRadius)
					{
						f = 1.0f - (d / avoidanceRadius);
						
						if(d > 0) 
							avgVelocity += (forceV / d) * f * avoidanceForce;
					}
					
					//just keep the current distance with the leader
					f = d / followRadius;
					MermaidFlock tempOtherFlock = otherFlocks[i];
					avgVelocity += tempOtherFlock.normalizedVelocity * f * followVelocity;	
				}
			}	
		}
		AvoidanceAndFollowPlayer();
		AvoidObstacles(ref origin);
		if(count > 0)
		{
			//Calculate the average flock velocity(Alignment)
			avgVelocity /= count;
			
			//Calculate Center value of the flock(Cohesion)
			toAvg = (avgPosition / count) - myPosition;	
		}	
		else
		{
			toAvg = Vector3.zero;		
		}
		//------------------------origin Setting-------------------------------
		//Directional Vector to the leader
		forceV = origin -  myPosition;
		d = forceV.magnitude;   
		f = d / toOriginRange;
		
		//Calculate the velocity of the flock to the leader
		if(d > 0) 
			originPush = (forceV / d) * f * toOriginForce;
		
		if(speed < minSpeed && speed > 0)
		{
			velocity = (velocity / speed) * minSpeed;
		}
		
		wantedVel = velocity;
		
		//Calculate final velocity
		wantedVel -= wantedVel *  Time.deltaTime;	
		wantedVel += randomPush * Time.deltaTime;
		wantedVel += originPush * Time.deltaTime;
		wantedVel += avgVelocity * Time.deltaTime;
		wantedVel += toAvg.normalized * gravity * Time.deltaTime;
		
		//Final Velocity to rotate the flock into
		velocity = Vector3.RotateTowards(velocity, wantedVel, turnSpeed * Time.deltaTime, 100.00f);
		transformComponent.rotation = Quaternion.LookRotation(velocity);
		
		//Move the flock based on the calculated velocity
		transformComponent.Translate(velocity * Time.deltaTime, Space.World);
		
		//normalise the velocity
		normalizedVelocity = velocity.normalized;
	}
	
}