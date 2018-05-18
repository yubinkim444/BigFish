using UnityEngine;
using System.Collections;

public class WitchVideoCube: MonoBehaviour {
	public GameObject video;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("Player")) {
			Debug.Log("Enter");
			video.SendMessage("SecondMovieStart");
		}
	}
}
