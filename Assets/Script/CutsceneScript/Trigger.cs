using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    // public string sceneToLoad;  // Nama scene yang akan dipindahkan

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ganti "Player" dengan tag yang sesuai
        {
            LevelManager.Instance.LoadScene("LimpaiCave");
            MusicManager.Instance.StopMusic();
        }
    }
}
