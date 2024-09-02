using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string trackName;
    public AudioClip loopClip;
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;

    public MusicTrack GetTrackFromName(string trackName)
    {
        foreach (var track in tracks)
        {
            if (track.trackName == trackName)
            {
                return track;
            }
        }
        return default;
    }
}
