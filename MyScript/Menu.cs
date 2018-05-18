using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	
	void OnGUI()
	{
		if(GUI.Button(new Rect((Screen.width*4/8.0f), (Screen.height*4/5), Screen.width/4, 
		                       Screen.height/10), "Play"))
		{
			Application.LoadLevel("HowToPlay");
		}
		if(GUI.Button(new Rect((Screen.width*6/8.0f), (Screen.height*4/5), Screen.width/4, 
		                       Screen.height/10), "Exit Game"))
		{
			Application.Quit(); 
		}
	}
}

