using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Step : MonoBehaviour
{
    TextMeshProUGUI stepLeft;
    public static bool death;

    public int step;
    // Start is called before the first frame update
    void Start()
    {
        stepLeft = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void StepLeft()
    {
        step--;
        stepLeft.text = "" + step;
        print(step);
        if (step <= 0){
            death = true;
            //SceneManager.LoadScene("Level1Fix1");
        }
    }
}
