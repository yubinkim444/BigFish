using UnityEngine;
using System.Collections;

public class Mission2Cube : MonoBehaviour {
	public GameObject mission2Camera;
	public GameObject audioController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("Player")) {
			mission2Camera.SendMessage("PlayerCome");
			audioController.SendMessage("EyeWin");
		}
	}
}
