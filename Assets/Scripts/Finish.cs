using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	public static bool finish = false, end;
	public GameObject Beres;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			MenuResult();
			// Debug.Log("Kena");
		}
	}

	public void MenuResult(){
		Beres.SetActive(true);
		finish = true;
		PlayerController.player = true;
    }
}
