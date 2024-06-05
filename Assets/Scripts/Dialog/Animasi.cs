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

        if(Finish.finish){
        animator.Play(Animation); // Pastikan nama animasi sesuai
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.Return) || stateInfo.normalizedTime >= 1.0f)
        {
            // Animasi saat ini selesai, nonaktifkan teks saat ini
            textFields[currentTextIndex].gameObject.SetActive(false);

            // Pindah ke teks berikutnya
            currentTextIndex++;
            print(currentTextIndex);
            if (currentTextIndex < totalTexts)
            {
                // Aktifkan teks berikutnya dan mainkan animasi berikutnya
                textFields[currentTextIndex].gameObject.SetActive(true);
            }
        }

        if(totalTexts == 1 && stateInfo.normalizedTime >= 1.0f){
            transisi.SetActive(true);
            if(prolog == true){
                endProlog = true;
            }
            
            Finish.finish = false;
            Finish.end = true;
        }
    }
}
