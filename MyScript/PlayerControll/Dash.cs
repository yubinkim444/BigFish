using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	public AudioSource dashAudio;
	private float dashSpeed=40.0f;
	private bool dash= false; 
	public float dashTimer=0.8f;
	private float dashTime=0.0f;
	public float minimumDistToAvoid = 20.0f;
	public float force = 50.0f;
	private float dashReloadTime = 0.0f;
	public float dashReloadTimer=5.0f;
	private bool reload = true;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftShift)&&reload) {
			dashAudio.Play();
			dash = true;
			reload = false;
		}
		if (dash) {
			if (dashTime >= dashTimer) {
					dashTime = 0;
					dash = false;
			}

			dashTime += Time.deltaTime;
			transform.Translate (Vector3.forward.normalized* Time.deltaTime * dashSpeed);

		}
		if (dashReloadTime >= dashReloadTimer) {
			dashReloadTime = 0;
			reload = true;
		}
		dashReloadTime += Time.deltaTime;
	}
}
