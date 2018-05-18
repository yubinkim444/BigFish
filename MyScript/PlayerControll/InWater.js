#pragma strict
public var underWaterColor : Color;
public var normalColor : Color;
public var gravity : float;
public var fallSpeed: float;
public var forwardSpeed : float;
public var sidewaySpeed : float;
public var foggyDensity : float;
public var swimSpeed : float;
private var characterMotor : CharacterMotor;
private var underWater : boolean;
private var targetSpeed : float;
private var item : boolean;
function OnTriggerEnter (Water : Collider)
{
	if( Water.CompareTag("Water")){
	 	underWater = true;
		characterMotor = GetComponent(CharacterMotor);
		characterMotor.movement.gravity = gravity;
		characterMotor.movement.maxFallSpeed = fallSpeed;
		targetSpeed = swimSpeed;
		characterMotor.movement.maxForwardSpeed = forwardSpeed;
		characterMotor.movement.maxSidewaysSpeed = sidewaySpeed;
		}
}

function Start () {
	underWater = false;
	normalColor = RenderSettings.fogColor;
}
function EatItem(){
	Debug.Log("EatItem");
	item = true;
}
function Update () {
var curSpeed :float = 0;
	if(underWater){  
		if(Input.GetKey(KeyCode.E)){
			curSpeed = Mathf.Lerp(curSpeed, targetSpeed, 60.0f * Time.deltaTime);
        	transform.Translate(new Vector3(0,1,0)* Time.deltaTime * curSpeed);  
		}
		
		
	}
} 











