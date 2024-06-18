using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimasiCerita : MonoBehaviour
{
     public Animator animator;
    public Image[] images; // Array untuk menampung empat Image
    private int currentImageIndex = 0;
    private int totalImages;
    public string Animation;
    public static bool startCerita;

    void Start()
    {
        totalImages = images.Length;
        startCerita = true;

        // if(Finish.finish){
        //     animator.Play(Animation); // Pastikan nama animasi sesuai
        //     print("BERES");
        // }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f || Input.anyKeyDown)
        {
            // Animasi saat ini selesai, nonaktifkan gambar saat ini
            images[currentImageIndex].gameObject.SetActive(false);

            if (currentImageIndex < (totalImages - 2))
            {
                currentImageIndex++;
                // Aktifkan gambar berikutnya dan mainkan animasi berikutnya
                images[currentImageIndex].gameObject.SetActive(true);
            }
            // if (currentImageIndex == totalImages){
            //     images[currentImageIndex].gameObject.SetActive(true);
            //     currentImageIndex = totalImages;
            // }
        }
    }
}
