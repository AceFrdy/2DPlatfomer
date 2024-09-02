using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneLinker : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;
    public string nextSceneName;

    void Start()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
            cutsceneDirector.stopped += OnCutsceneEnd;
        }
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        // Muat scene berikutnya setelah cutscene selesai
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
