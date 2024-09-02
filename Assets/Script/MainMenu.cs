using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    private string[] stages = { "Intro1", "Intro2", "Intro3" };

    private void Start()
    {
        LoadVolume();
        UpdateMusicVolume(musicSlider.value);  // Pastikan nilai slider diatur ke AudioMixer
        UpdateSoundVolume(sfxSlider.value);
        MusicManager.Instance.PlayMusic("MainMenu");
    }


    public void Play(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stages.Length)
        {
            string selectedStage = stages[stageIndex];
            // Panggil fungsi LoadScene dari LevelManager
            SoundManager.Instance.PlaySound2D("ClickStage");
            LevelManager.Instance.LoadScene(selectedStage, "CrossFade");
            PlayerPrefs.DeleteKey("HasPlayedCutscene");
            // LevelManager.Instance.LoadScene("Level1");
        }
        else
        {
            Debug.LogError("Invalid stage index");
        }
    }

    public void Settings()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        float defaultVolume = 0f; // Volume default yang masuk akal
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", defaultVolume);
    }


    public void MuteMusic()
    {
        audioMixer.SetFloat("MusicVolume", -80f); // Menurunkan volume ke level yang sangat rendah
    }

    public void UnmuteMusic()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        audioMixer.SetFloat("MusicVolume", musicVolume); // Mengembalikan volume ke nilai yang disimpan
    }

}
