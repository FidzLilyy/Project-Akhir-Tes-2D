using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Animasi : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI[] textFields; // Array untuk menampung empat TextMeshProUGUI
    private int currentTextIndex = 0;
    private int totalTexts;
    public static bool endProlog = false, prolog = true;

    public GameObject transisi;

    public string Animation;

    void Start()
    {
        totalTexts = textFields.Length;

        // Pastikan hanya teks pertama yang terlihat di awal
        for (int i = 0; i < totalTexts; i++)
        {
            textFields[i].gameObject.SetActive(i == 0);
        }

        // if(Finish.finish){
        //     animator.Play(Animation); // Pastikan nama animasi sesuai
        //     print("BERES");
        // }
        animator.Play(Animation);
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f || Input.anyKeyDown)
        {
            // Animasi saat ini selesai, nonaktifkan teks saat ini
            textFields[currentTextIndex].gameObject.SetActive(false);

            // Pindah ke teks berikutnya
            currentTextIndex++;

            if (currentTextIndex < totalTexts)
            {
                // Aktifkan teks berikutnya dan mainkan animasi berikutnya
                textFields[currentTextIndex].gameObject.SetActive(true);                
            }
        }

        if(currentTextIndex >= totalTexts){
                endProlog = true;
                transisi.SetActive(true);
            //Finish.finish = false;
        }
    }
}
