using UnityEngine;
using System.Collections;

public class Scene1Video : MonoBehaviour {
	public GameObject audioController;

	public MovieTexture movie1;
	public MovieTexture movie2;
	public GameObject cameraPos;
	public GameObject subCamera;

	public AudioClip audio1;
	public AudioClip audio2;
	public Underwater uw;
	private AudioSource audi;
	private float fogDensity;
	private float skyfogDensity;
	private bool first = true;
	private bool start = false;

	private bool oneShot =false;
	// Use this for initialization
	void Start () {
		fogDensity = uw.waterFogDensity;
		skyfogDensity = uw.skyFogDensity;

	}
	
	void Update(){
		if (first) {
			renderer.material.mainTexture = movie1 as MovieTexture;
			GetComponent<AudioSource>().clip = audio1;
			if (start) {
				movie1.Play ();
				GetComponent<AudioSource>().Play();
				start = false;
				oneShot = true;
			}
			if(!movie1.isPlaying&&oneShot) {
				GetComponent<AudioSource>().Stop();
				oneShotSet();
			}
		} 
		else {
			renderer.material.mainTexture = movie2 as MovieTexture;
			GetComponent<AudioSource>().clip = audio2;
			if (start) {
				movie2.Play();
				GetComponent<AudioSource>().Play();
				start = false;
				oneShot = true;
			}
			if(!movie2.isPlaying&&oneShot) {
				GetComponent<AudioSource>().Stop();
				oneShotSet();
			}
		}
	}
	void oneShotSet(){
		if (oneShot) {
			subCamera.SendMessage("PlayerOut");
			audioController.SendMessage("VideoEnd");
			RenderSettings.fog = true;
			uw.waterFogDensity= fogDensity;
			uw.skyFogDensity = skyfogDensity;
			oneShot = false;

		}
	}

	void FirstMovieStart(){
		first = true;
		start = true;
		subCamera.transform.position = cameraPos.transform.position;
		Vector3 lookEye =  transform.position -  cameraPos.transform.position;
		subCamera.transform.rotation = Quaternion.LookRotation(lookEye);
		uw.waterFogDensity = 0.0f;
		uw.skyFogDensity = 0.0f;
		RenderSettings.fog = false;
		subCamera.SendMessage ("PlayerCome");
		audioController.SendMessage("VideoStart");
	}
	void SecondMovieStart(){
		first = false;
		start = true;
		subCamera.transform.position = cameraPos.transform.position;
		Vector3 lookEye =  transform.position -  cameraPos.transform.position;
		subCamera.transform.rotation = Quaternion.LookRotation(lookEye);
		uw.waterFogDensity = 0.0f;
		uw.skyFogDensity = 0.0f;
		RenderSettings.fog = false;
		subCamera.SendMessage ("PlayerCome");
		audioController.SendMessage("VideoStart");
	}
}
