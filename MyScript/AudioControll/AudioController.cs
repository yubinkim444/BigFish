using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource bubbleAudio;
	public AudioSource backgroundAudio;
	public AudioSource mermaidSing;
	public AudioSource EyeTalk;
	public AudioSource ItemEat1;
	public AudioSource ItemEat2;
	// Use this for initialization
	void Start () {
		bubbleAudio.Play();
		backgroundAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void VideoStart(){
		bubbleAudio.Pause();
		backgroundAudio.Pause();
	}
	void VideoEnd(){
		bubbleAudio.Play();
		backgroundAudio.Play();
	}
	void MermaidSing(){
		mermaidSing.Play();
	}
	void EyeWin(){
		EyeTalk.Play();
	}

}
