using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject objectToToggle, panel;
    public string level, nLevel;

    public static bool endLevel = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu();
        }else if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(level);
        }
    }

    public void pauseMenu(){
		objectToToggle.SetActive(true);
        panel.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		objectToToggle.SetActive(false);
        panel.SetActive(false);
		Time.timeScale = 1f;
	}

    public void restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void NextLevel(){
        Time.timeScale = 1f;
        endLevel = true;
        print(endLevel);
        if(!endLevel){
            SceneManager.LoadScene(nLevel);
        }
    }
}
