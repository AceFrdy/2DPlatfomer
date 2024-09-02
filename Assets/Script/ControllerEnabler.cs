using UnityEngine;

public class ControlEnablerDalamRumah : MonoBehaviour
{
    public GameObject player;     // Reference ke GameObject Player
    public GameObject healthUI;   // Reference ke UI Health
    public GameObject NPC;   // Reference ke UI Health

    public void EnableControls()
    {
        if (player != null && healthUI != null && NPC != null)
        {
            // Aktifkan kembali GameObject Player dan HealthUI
            player.SetActive(true);
            healthUI.SetActive(true);
            NPC.SetActive(true);
        }
    }
}
