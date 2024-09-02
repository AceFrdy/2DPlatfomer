using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStarter : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;

    void Start()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play();
        }
        else
        {
            Debug.LogWarning("PlayableDirector not assigned!");
        }
    }
}
