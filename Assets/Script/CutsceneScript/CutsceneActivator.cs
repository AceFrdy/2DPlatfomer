using UnityEngine;

public class CutsceneActivator : MonoBehaviour
{
    public GameObject cutsceneObject;  // GameObject untuk cutscene yang akan diaktifkan

    private void OnTriggerEnter(Collider other)
    {
        // Pastikan yang masuk ke dalam trigger adalah pemain
        if (other.CompareTag("Player"))
        {
            // Aktifkan objek cutscene
            if (cutsceneObject != null)
            {
                cutsceneObject.SetActive(true);
            }
            else
            {
                Debug.LogError("CutsceneObject reference is not assigned in the inspector.");
            }

            // Optional: Menonaktifkan collider trigger jika hanya ingin memicu sekali
            // gameObject.SetActive(false);
        }
    }
}
