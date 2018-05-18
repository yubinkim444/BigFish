using UnityEngine;
using System.Collections;

public class Mission2 : MonoBehaviour {

	public Material winMat;
	private bool win = false;
	private bool startTimer = false;
	private float timer=1.5f;
	private float time =0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {

			time += Time.deltaTime;
			if(time>= timer){
				Application.LoadLevel("FatherSick");
			}
		}
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("Player")&&win) {
			this.gameObject.transform.GetChild(1).renderer.material = winMat;
			startTimer = true;
		}
	}
	void PlayerWin(){
		win = true;
	}
}
