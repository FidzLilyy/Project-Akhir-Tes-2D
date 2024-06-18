using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AnimasiGambar : MonoBehaviour
{
    public string scene;
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
        if(Animasi.endProlog == true || PlayerController.player == true || AnimasiCerita.startCerita == true){
            timeElapsed += Time.deltaTime;

                // Hitung t berdasarkan waktu yang telah berlalu dan durasi
                float t = timeElapsed / duration;

                // Gunakan kurva untuk mengatur t
                float curveT = curve.Evaluate(t);

                // Lerp posisi berdasarkan curveT
                transform.position = Vector3.Lerp(startPosition, endPosition, curveT);
            if(Finish.finish || Animasi.endProlog == true){
                timeElapsed += Time.deltaTime;

                // Hitung t berdasarkan waktu yang telah berlalu dan durasi
                t = timeElapsed / duration;

                // Gunakan kurva untuk mengatur t
                curveT = curve.Evaluate(t);

                // Lerp posisi berdasarkan curveT
                transform.position = Vector3.Lerp(endPosition, startPosition, curveT);
            }

            // if(Finish.end || level == "Level0Fix" || PlayerController.transisiScene == true){
            //     textS.text = text2;
            //     //gameObject.SetActive(true);

            // // Perbarui waktu yang telah berlalu
            //     timeElapsed += Time.deltaTime;

            //     // Hitung t berdasarkan waktu yang telah berlalu dan durasi
            //     float t = timeElapsed / duration;

            //     // Gunakan kurva untuk mengatur t
            //     float curveT = curve.Evaluate(t);

            //     // Lerp posisi berdasarkan curveT
            //     transform.position = Vector3.Lerp(endPosition,startPosition, curveT);
            // }
            // if(!Finish.finish){
            //     //gameObject.SetActive(true);

            //     // Perbarui waktu yang telah berlalu
            //     timeElapsed += Time.deltaTime;

            //     // Hitung t berdasarkan waktu yang telah berlalu dan durasi
            //     t = timeElapsed / duration;

            //     // Gunakan kurva untuk mengatur t
            //     curveT = curve.Evaluate(t);

            //     // Lerp posisi berdasarkan curveT
            //     transform.position = Vector3.Lerp(startPosition, endPosition, curveT);
            // }


            if (timeElapsed > duration)
            {
                timeElapsed = 0.0f;
                PlayerController.player = false;
                Pause.endLevel = false;
                AnimasiCerita.startCerita = false;
                if(Finish.finish){
                    Finish.finish = false;
                    SceneManager.LoadScene(scene);
                }

                // if(Finish.end){
                //     Finish.end = false;
                //     Finish.finish = false;
                // } 

                if(Animasi.endProlog == true){
                    Animasi.endProlog = false;
                    Animasi.prolog = false;
                    SceneManager.LoadScene(scene);
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