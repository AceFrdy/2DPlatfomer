using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private MainMenu mainMenu;

    private void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();

        if (mainMenu != null)
        {
            mainMenu.MuteMusic(); // Mute music saat cutscene dimulai
        }

        // Logika cutscene Anda di sini
    }

    private void EndCutscene()
    {
        if (mainMenu != null)
        {
            mainMenu.UnmuteMusic(); // Unmute music saat cutscene selesai
        }
    }
}
