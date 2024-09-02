using UnityEngine;

public class ControlEnabler : MonoBehaviour
{
    public GameObject Music;   // Reference ke UI Health

    public void EnableControls()
    {
        if ( Music != null)
        {
            // Aktifkan kembali GameObject Player dan HealthUI
            Music.SetActive(true);
        }
    }
}
