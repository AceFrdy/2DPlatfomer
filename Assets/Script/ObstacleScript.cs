using UnityEngine;

public class InstantDeathObstacle : MonoBehaviour
{
    public int instantDeathDamage = 1000; // Damage yang cukup besar untuk mengurangi health menjadi 0

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                Vector2 knockback = Vector2.zero; // Sesuaikan knockback jika diperlukan
                damageable.TakeDamage(instantDeathDamage, knockback);
            }
        }
    }
}
