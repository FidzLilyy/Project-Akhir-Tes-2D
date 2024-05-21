using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	public string level;
	public GameObject panel, Beres;
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			MenuResult();
			// Debug.Log("Kena");
		}
	}

	public void MenuResult(){
        Time.timeScale = 0f;
        panel.SetActive(true);
		Beres.SetActive(true);
        Time.timeScale = 0f;
    }
}
