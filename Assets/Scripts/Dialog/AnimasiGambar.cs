using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnimasiGambar : MonoBehaviour
{
    public string level;
    public AnimationCurve curve;
    public TextMeshProUGUI textS;
    public string text2;

    // Waktu yang diperlukan untuk interpolasi
    public float duration = 2.0f;

    // Posisi awal dan akhir
    public Vector3 startPosition;
    public Vector3 endPosition, fixPosition;

    // Variabel untuk melacak waktu
    private float timeElapsed = 0.0f;

    void Update()
    {
        // if(transform.position == endPosition){
        //     gameObject.SetActive(false);
        //     }
        if(Animasi.endProlog == true || PlayerController.player == true){
            if(Finish.end || level == "Level0Fix" || PlayerController.transisiScene == true){
                textS.text = text2;
                //gameObject.SetActive(true);

            // Perbarui waktu yang telah berlalu
                timeElapsed += Time.deltaTime;

                // Hitung t berdasarkan waktu yang telah berlalu dan durasi
                float t = timeElapsed / duration;

                // Gunakan kurva untuk mengatur t
                float curveT = curve.Evaluate(t);

                // Lerp posisi berdasarkan curveT
                transform.position = Vector3.Lerp(endPosition,startPosition, curveT);
            }else if(!Finish.finish){
                //gameObject.SetActive(true);

            // Perbarui waktu yang telah berlalu
                timeElapsed += Time.deltaTime;

                // Hitung t berdasarkan waktu yang telah berlalu dan durasi
                float t = timeElapsed / duration;

                // Gunakan kurva untuk mengatur t
                float curveT = curve.Evaluate(t);

                // Lerp posisi berdasarkan curveT
                transform.position = Vector3.Lerp(startPosition, endPosition, curveT);
            }
            //fixPosition = transform.position;


            if (timeElapsed > duration)
            {
                timeElapsed = 0.0f;
                PlayerController.player = false;
                Pause.endLevel = false;

                if(Finish.end){
                    SceneManager.LoadScene(level);
                    Finish.end = false;
                    Finish.finish = false;
                } 

                if(Animasi.endProlog == true){
                    if(level == "Level0Fix"){
                        SceneManager.LoadScene(level);
                    }
                    Animasi.endProlog = false;
                    Animasi.prolog = false;
                }else if(PlayerController.player = true){
                    print("pppp");
                    PlayerController.player = false;
                    //gameObject.SetActive(false);
                }

                if(PlayerController.transisiScene){
                    Step.death = false;
                }
            }
        }
    }
}