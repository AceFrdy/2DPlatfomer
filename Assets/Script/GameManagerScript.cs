using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    // static GameManagerScript current;
    public GameObject gameOverUI;
    public GameObject winMenuUI;

    // public GameObject player;
    public GameObject healthBarUI;
    public GameObject pausedMenuUI;

    public bool isPaused;
    public Transform spawnPoint; // Drag the SpawnPoint GameObject here in the Inspector
    public GameObject player;

    // List<Gem> gem;
    // Door lockedDoor;
    // SceneFader sceneFader;

    void Start()
    {
        pausedMenuUI.SetActive(false);
        SpawnPlayer();
    }
    // void Awake()
    // {
    //     current = this;
    //     gem = new List<Gem>();
    // }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void SpawnPlayer()
    {
        if (spawnPoint != null && player != null)
        {
            player.transform.position = spawnPoint.position;
            // Optionally reset player's health or other properties here
        }
    }

    //==================Game Over Menu=============================

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        MusicManager.Instance.StopMusic();  // Stop music when game over
        SoundManager.Instance.PlaySound2D("GameOver");
        if (healthBarUI != null)
        {
            healthBarUI.SetActive(false); // Menyembunyikan health bar UI
        }
        SoundManager.Instance.PlaySound2D("DeathSFX");
    }

     public void restart()
    {
        // Cek apakah cutscene sudah diputar atau belum
        // if (PlayerPrefs.GetInt("HasPlayedCutscene", 0) == 1)
        // {
        //     // Skip cutscene jika sudah pernah diputar
        //     CutsceneManager cutsceneManager = FindObjectOfType<CutsceneManager>();
        //     if (cutsceneManager != null)
        //     {
        //         cutsceneManager.SkipCutscene();
        //     }
        // }

        // Lakukan restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.PlaySound2D("Click");
        SoundManager.Instance.StopSound();
        MusicManager.Instance.StopMusic();
    }
    public void mainMenu()
    {
        Debug.Log("MainMenu called");
        LevelManager.Instance.LoadScene("MainMenu", "CrossFade");
        SoundManager.Instance.PlaySound2D("Click");
        SoundManager.Instance.StopSound();
    }
    public void Quit()
    {
        Application.Quit();
        SoundManager.Instance.PlaySound2D("Click");
    }
    //================================================================

    //==================Pause Menu====================================

    public void PauseGame()
    {
        SoundManager.Instance.PlaySound2D("Click");
        pausedMenuUI.SetActive(true);

        // Set Animator Update Mode to Unscaled Time
        Animator[] animators = pausedMenuUI.GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        SoundManager.Instance.PlaySound2D("Click");
        pausedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseMainMenu()
    {
        ResumeGame(); // Ini akan mengembalikan game dari keadaan pause
        LevelManager.Instance.LoadScene("MainMenu", "CrossFade");
        SoundManager.Instance.PlaySound2D("Click");
    }
    public void PauseQuitMenu()
    {
        ResumeGame(); // Ini akan mengembalikan game dari keadaan pause
        Application.Quit();
        SoundManager.Instance.PlaySound2D("Click");
    }

    public void PlayerWon()
    {
        if (player != null)
        {
            player.SetActive(false); // Menyembunyikan health bar UI
        }
        MusicManager.Instance.StopMusic();  // Stop music when player wins
        SoundManager.Instance.PlaySound2D("WinSound");
        winMenuUI.SetActive(true);
    }
}
