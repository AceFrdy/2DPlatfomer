using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pemain memasuki trigger " + gameObject.name);

            if (gameObject.name == "DiluarRumah")
            {
                Debug.Log("Memuat level sebelumnya");
                LoadLevelByName("Level1"); // Misalnya memuat level TestHome
            }
            else if (gameObject.name == "DalamRumah")
            {
                LoadLevelByName("DalamRumah"); // Misalnya memuat level DalamRumah
            }
        }
    }

    // Memuat level berdasarkan nama scene
    public void LoadLevelByName(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    // Coroutine untuk menjalankan transisi dan memuat scene
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
