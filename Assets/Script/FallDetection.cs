using UnityEngine;

public class FallDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Menghapus player setelah jatuh
            Destroy(other.gameObject);

            // Memanggil Game Over dari GameManagerScript
            FindObjectOfType<GameManagerScript>().gameOver();
        }
    }
}
