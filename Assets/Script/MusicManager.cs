using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource loopSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetTrackFromName(trackName), fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(MusicTrack nextTrack, float fadeDuration = 0.5f)
    {
        if (nextTrack.loopClip == null)
        {
            Debug.LogError("Next track loop clip is null!");
            yield break;
        }

        // Fade out current track
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            loopSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        Debug.Log("Starting next track: " + nextTrack.loopClip.name);

        // Play loop clip
        loopSource.clip = nextTrack.loopClip;
        loopSource.loop = true;
        loopSource.Play();

        // Fade in loop clip
        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            loopSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }

    public void StopMusic(float fadeDuration = 0.5f)
    {
        StartCoroutine(FadeOutMusic(fadeDuration));
        
    }

    private IEnumerator FadeOutMusic(float fadeDuration)
    {
        float startVolumeLoop = loopSource.volume;
        float percent = 0;

        // Fade out the music
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            loopSource.volume = Mathf.Lerp(startVolumeLoop, 0, percent);
            yield return null;
        }

        // Stop the audio source
        loopSource.Stop();

        // Ensure volume is reset
        loopSource.volume = 1f;
    }
}
