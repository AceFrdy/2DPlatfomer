using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;      // Reference ke GameObject Player
    public GameObject healthUI;    // Reference ke UI Health
    public GameObject cutscene;    // Reference ke GameObject Cutscene atau animasi cutscene
    private bool isCutsceneActive;

    void Start()
    {
        if (cutscene != null)
        {
            cutscene.SetActive(false); // Pastikan cutscene tidak aktif di awal
        }
        isCutsceneActive = false;
    }

    void Update()
    {
        // Cek jika cutscene sedang berjalan dan objek tidak null
        if (isCutsceneActive && cutscene != null)
        {
            // Sembunyikan player dan UI saat cutscene aktif
            if (player != null)
                player.SetActive(false);
            if (healthUI != null)
                healthUI.SetActive(false);
        }
        else
        {
            // Tampilkan player dan UI saat cutscene tidak aktif
            if (player != null)
                player.SetActive(true);
            if (healthUI != null)
                healthUI.SetActive(true);
        }
    }

    // Fungsi ini dipanggil untuk memulai cutscene
    public void StartCutscene()
    {
        if (cutscene != null)
        {
            isCutsceneActive = true;
            cutscene.SetActive(true);
        }
    }

    // Fungsi ini dipanggil untuk mengakhiri cutscene
    public void EndCutscene()
    {
        if (cutscene != null)
        {
            isCutsceneActive = false;
            cutscene.SetActive(false);
        }
    }
}
