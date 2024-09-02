using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private SoundLibrary sfxLibrary;
    [SerializeField]
    private AudioSource sfx2DSource;

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

    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }
 
    public void PlaySound2D(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            sfx2DSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null 2D clip for soundName: " + soundName);
        }
    }
    public void StopSound()
    {
        if (sfx2DSource.isPlaying)
        {
            sfx2DSource.Stop();
        }
    }

    public void PlayClickSound()
    {
        PlaySound2D("Click");
    }

    public void PlayHoverSound()
    {
        PlaySound2D("Hover");
    }
    public void PlayPickupSound()
    {
        PlaySound2D("ClickStage");
    }
}
