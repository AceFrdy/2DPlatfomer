using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject door;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public float floatAmplitude = 0.5f; // Amplitudo gerakan atas-bawah
    public float floatFrequency = 1f; // Frekuensi gerakan atas-bawah

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Gem picked up");

            DoorEnd doorEnd = door.GetComponent<DoorEnd>();
            if (doorEnd != null)
            {
                SoundManager.Instance.PlaySound2D("DoorKeyOpen");
                doorEnd.OpenDoor();
            }

            // Setelah gem diambil, gem bisa dihancurkan atau dihilangkan dari scene
            Destroy(gameObject);
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
