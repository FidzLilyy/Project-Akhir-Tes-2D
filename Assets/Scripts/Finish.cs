using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	public static bool finish = false;
	public string scene;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			MenuResult();
			// Debug.Log("Kena");
		}
	}

	public void MenuResult(){
		finish = true;
		PlayerController.player = true;
    }
}
