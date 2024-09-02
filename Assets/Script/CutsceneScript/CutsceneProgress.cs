using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneProgress : MonoBehaviour
{
    public int cutsceneIndex;

    void Start()
    {
        cutsceneIndex = PlayerPrefs.GetInt("CutsceneIndex", 0);
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        // Update cutscene progress
        cutsceneIndex++;
        PlayerPrefs.SetInt("CutsceneIndex", cutsceneIndex);

        // Load the next scene based on cutsceneIndex
        LoadNextScene();
    }

    void LoadNextScene()
    {
        switch (cutsceneIndex)
        {
            case 1:
                SceneManager.LoadScene("Scene1");
                break;
            case 2:
                SceneManager.LoadScene("Scene2");
                break;
            case 3:
                SceneManager.LoadScene("Scene3");
                break;
            default:
                // Back to gameplay or any default scene
                SceneManager.LoadScene("GameplayScene");
                break;
        }
    }
}
