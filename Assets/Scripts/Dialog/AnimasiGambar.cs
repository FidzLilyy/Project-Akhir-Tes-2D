using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimasiGambar : MonoBehaviour
{
    public GameObject transisi;
    // Definisikan kurva melalui Inspector
    public AnimationCurve curve;

    // Waktu yang diperlukan untuk interpolasi
    public float duration = 2.0f;

    // Posisi awal dan akhir
    public Vector3 startPosition;
    public Vector3 endPosition;

    // Variabel untuk melacak waktu
    private float timeElapsed = 0.0f;

    void Update()
    {
        if(Animasi.endProlog == true || PlayerController.player == true){
            // Perbarui waktu yang telah berlalu
            timeElapsed += Time.deltaTime;

            // Hitung t berdasarkan waktu yang telah berlalu dan durasi
            float t = timeElapsed / duration;

            // Gunakan kurva untuk mengatur t
            float curveT = curve.Evaluate(t);

            // Lerp posisi berdasarkan curveT
            transform.position = Vector3.Lerp(startPosition, endPosition, curveT);

            // Jika sudah mencapai atau melebihi durasi, reset waktu
            if (timeElapsed >= duration)
            {
                timeElapsed = 0.0f;
                PlayerController.player = false;
                if(Animasi.endProlog == true){
                    SceneManager.LoadScene("Level0Fix");
                    Animasi.endProlog = false;
                }else if(PlayerController.player = true){
                    PlayerController.player = false;
                    transisi.SetActive(false);
                }
            }
        }
    }
}