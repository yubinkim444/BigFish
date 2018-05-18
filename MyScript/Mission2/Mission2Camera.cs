using UnityEngine;
using System.Collections;

public class Mission2Camera : MonoBehaviour {

	public GameObject subCamera;
	public GameObject mainCamera;
	public GameObject eye;
	public float playTime= 5.0f;
	public GameObject mission2Plane;
	private float playTimer = 0.0f;
	private bool playerCome = false;
	private Vector3 lookEye;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(playerCome) {
			subCamera.SendMessage("PlayerCome");
			subCamera.transform.position = transform.position;
			lookEye =  eye.transform.position - transform.position;
			subCamera.transform.rotation = Quaternion.LookRotation(lookEye);
			mission2Plane.renderer.enabled = true;
			playTimer += Time.deltaTime;
			if(playTimer >= playTime){
				playerCome =false;
				subCamera.SendMessage("PlayerOut");
				mission2Plane.renderer.enabled = false;
			}
		} 

	}
	void PlayerCome(){
		Debug.Log("a");
		playerCome = true;
	}
}
