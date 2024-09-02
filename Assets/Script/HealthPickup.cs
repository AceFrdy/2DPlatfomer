using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public float floatAmplitude = 0.5f; // Amplitudo gerakan atas-bawah
    public float floatFrequency = 1f; // Frekuensi gerakan atas-bawah

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        Damageable damageable = collison.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                SoundManager.Instance.PlaySound2D("Pickup");
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        // Rotasi
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;

        // Gerakan atas-bawah
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = initialPosition + new Vector3(0, yOffset, 0);
    }
}