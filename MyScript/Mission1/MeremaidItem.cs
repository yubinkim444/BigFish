using UnityEngine;
using System.Collections;

public class MeremaidItem : MonoBehaviour {
	public GameObject mermaid;
	public GameObject particle;
	public AudioSource audioC;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag("Player")) {
			mermaid.SendMessage("EatItem");
			audioC.Play();
			Instantiate(particle,transform.position,Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
