using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;
    public float speedMultiplier = 2f; // Faktor pengali kecepatan

    public void FireProjectile()
    {
        // Instansiasi projectile
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        // Atur skala projectile
        Vector3 origScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            origScale.x * (transform.localScale.x > 0 ? 1 : -1),
            origScale.y,
            origScale.z
        );

        // Atur kecepatan projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(projectileScript.moveSpeed.x * transform.localScale.x * speedMultiplier, projectileScript.moveSpeed.y * speedMultiplier);
            }
        }
    }
}
