using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	public GameObject light;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider player){
		if (player.CompareTag ("Player")) {
			light.light.intensity = 0.0f;
		}
	}
}
