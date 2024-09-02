using UnityEngine;
using Cinemachine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera gameplayCam;    // Virtual Camera untuk gameplay
    public CinemachineVirtualCamera cutsceneCam;    // Virtual Camera untuk cutscene
    public float cutsceneDuration = 5f;             // Durasi cutscene dalam detik

    private bool hasTriggered = false;  // Flag untuk memastikan cutscene hanya dipicu sekali

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan yang masuk ke dalam trigger adalah pemain dan cutscene belum dipicu
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;  // Tandai bahwa cutscene telah dipicu

            // Set prioritas kamera cutscene lebih tinggi sehingga aktif
            cutsceneCam.Priority = 20;
            gameplayCam.Priority = 10;

            // Mulai coroutine untuk mengakhiri cutscene setelah durasi tertentu
            StartCoroutine(EndCutsceneAfterDelay(cutsceneDuration));
        }
    }

    private IEnumerator EndCutsceneAfterDelay(float delay)
    {
        // Tunggu selama durasi cutscene
        yield return new WaitForSeconds(delay);

        // Kembalikan prioritas ke kamera gameplay
        gameplayCam.Priority = 20;
        cutsceneCam.Priority = 10;

        // Mengatur flag agar trigger tidak aktif lagi
        hasTriggered = false;
    }
}
