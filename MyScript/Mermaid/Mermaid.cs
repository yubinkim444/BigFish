using UnityEngine;
using System.Collections;

public class Mermaid : MonoBehaviour {
	public GameObject audioController;
	public GameObject area2;
	public GameObject talkPlane;
	public GameObject missionCamera;
	public GameObject cameraPos;
	public GameObject missionPlane;
	public Material winTalk;
	private bool idle = true;
	private bool item = false;
	private float dethTime = 10.0f;
	private float dethTimer = 0.0f;
	private bool win = false;

	// Use this for initialization
	void Start () {
		StartCoroutine (IdleAni ());
	}
	IEnumerator IdleAni()
	{
		while(true)
		{	
			if(idle){
				animation.Play ("Sit idle");
			}
			else{idle = true; }
			yield return new WaitForSeconds(10);
		}
	}
	void EatItem(){
		Debug.Log("EatItem");
		item = true;
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag("Player")) {

			if(item){
			missionCamera.transform.position = cameraPos.transform.position;
			Vector3 lookEye =  transform.position -  cameraPos.transform.position;
			missionCamera.transform.rotation = Quaternion.LookRotation(lookEye);
			win =true;
			idle =false;
			missionCamera.SendMessage("PlayerCome");
			animation.Play("Sit sing");	
			audioController.SendMessage("MermaidSing");
			talkPlane.gameObject.renderer.enabled =true;
			area2.collider.isTrigger = true;
			talkPlane.gameObject.renderer.material = winTalk;
			}
			else{
				idle =false;
				animation.Play("Sit talk");	
				talkPlane.gameObject.renderer.enabled =true;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (win) {
			dethTimer += Time.deltaTime;
			if(dethTimer >= dethTime){
				missionCamera.SendMessage("PlayerOut");
				talkPlane.gameObject.renderer.enabled = false;
				Destroy(this.gameObject);
			}
		}
	}
}
