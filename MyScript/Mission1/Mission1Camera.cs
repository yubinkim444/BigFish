using UnityEngine;
using System.Collections;

public class Mission1Camera : MonoBehaviour {
	public GameObject mission1Camera;
	public GameObject mainCamera;
	private bool playerCome = false;
	// Use this for initialization
	void Start () {
		mission1Camera.camera.enabled = false;
		mainCamera.camera.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (playerCome) {
			mission1Camera.camera.enabled = true;
			mainCamera.camera.enabled = false;
		} 
		else {
			mission1Camera.camera.enabled = false;
			mainCamera.camera.enabled = true;	
		}
	}
	void PlayerCome(){
		playerCome = true;
	}
	void PlayerOut(){
		playerCome = false;
	}
}
