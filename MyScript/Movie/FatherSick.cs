using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource)) ]

public class FatherSick : MonoBehaviour {
	
	
	public MovieTexture movie;
	
	// Use this for initialization
	void Start () {
		renderer.material.mainTexture = movie as MovieTexture;
		movie.Play();
	}
	
	void Update(){
		if(!movie.isPlaying || Input.GetKey (KeyCode.Escape)) 
			Application.LoadLevel("BigFish2");
	}
} 
