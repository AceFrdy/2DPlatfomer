using UnityEngine;

[System.Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}

public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;

    public AudioClip GetClipFromName(string name)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == name)
            {
                if (soundEffect.clips.Length > 0)
                {
                    return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
                }
                else
                {
                    Debug.LogWarning("No clips found for groupID: " + name);
                }
            }
        }
        Debug.LogWarning("No sound effect found for groupID: " + name);
        return null;
    }
}
