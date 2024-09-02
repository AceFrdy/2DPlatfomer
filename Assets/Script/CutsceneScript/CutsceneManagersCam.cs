using UnityEngine;
using Cinemachine;

public class CutsceneManagersCam : MonoBehaviour
{
    public CinemachineVirtualCamera gameplayCam;    // Kamera untuk gameplay
    public CinemachineVirtualCamera cutsceneCam1;   // Kamera untuk cutscene pertama
    // public CinemachineVirtualCamera cutsceneCam2;   // Kamera untuk cutscene kedua
    // public PlayableDirector cutsceneDirector;

    private void Start()
    {
        // Debugging: Cek apakah semua referensi sudah di-assign
        if (gameplayCam == null)
        {
            Debug.LogError("GameplayCam is not assigned in the inspector.");
        }
        if (cutsceneCam1 == null)
        {
            Debug.LogError("CutsceneCam1 is not assigned in the inspector.");
        }
        // if (cutsceneCam2 == null)
        // {
        //     Debug.LogError("CutsceneCam2 is not assigned in the inspector.");
        // }
       

        // Atur prioritas awal
        gameplayCam.Priority = 20;
        cutsceneCam1.Priority = 10;
        // cutsceneCam2.Priority = 10;
    }

    private void TriggerCutscene1()
    {
        // Mengatur prioritas cutscene 1 lebih tinggi
        cutsceneCam1.Priority = 30;
        gameplayCam.Priority = 10;

        // Optional: Nonaktifkan virtual camera lain jika tidak diperlukan
        // cutsceneCam2.enabled = false;
    }

    private void TriggerCutscene2()
    {
        // Mengatur prioritas cutscene 2 lebih tinggi
        // cutsceneCam2.Priority = 30;
        cutsceneCam1.Priority = 10;
        gameplayCam.Priority = 10;

        // Optional: Nonaktifkan virtual camera lain jika tidak diperlukan
        cutsceneCam1.enabled = false;
    }

    private void EndCutscene()
    {
        // Kembalikan prioritas ke gameplay camera setelah cutscene
        gameplayCam.Priority = 20;
        cutsceneCam1.Priority = 10;
        // cutsceneCam2.Priority = 10;

        // Optional: Aktifkan kembali gameplay camera dan nonaktifkan cutscene cameras
        gameplayCam.enabled = true;
        cutsceneCam1.enabled = false;
        // cutsceneCam2.enabled = false;
    }
}
