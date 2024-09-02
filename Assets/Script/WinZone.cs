using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Pastikan player memiliki tag "Player"
        {
            GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
            if (gameManager != null)
            {
                gameManager.PlayerWon();
            }
        }
    }
}
