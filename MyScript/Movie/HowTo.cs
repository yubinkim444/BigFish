using UnityEngine;
using System.Collections;

public class HowTo : MonoBehaviour {
	public TextMesh inform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			Application.LoadLevel ("BigFish1");
			inform.text = "Please Wait...";
		}
	}
}
