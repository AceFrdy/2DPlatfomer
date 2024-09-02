using UnityEngine;

public class DoorEnd : MonoBehaviour
{
    public Vector3 openPositionOffset = new Vector3(0, 5, 0);  // Offset posisi untuk membuka pintu
    public float openSpeed = 2f;  // Kecepatan pintu bergerak

    private Vector3 closedPosition;  // Posisi awal pintu
    private bool isOpening = false;  // Apakah pintu sedang membuka

    private void Start()
    {
        // Menyimpan posisi awal pintu
        closedPosition = transform.position;
    }

    private void Update()
    {
        if (isOpening)
        {
            // Geser pintu ke posisi terbuka
            transform.position = Vector3.MoveTowards(transform.position, closedPosition + openPositionOffset, openSpeed * Time.deltaTime);
            
            // Cek apakah pintu sudah mencapai posisi terbuka
            if (transform.position == closedPosition + openPositionOffset)
            {
                isOpening = false; // Stop membuka ketika sudah mencapai posisi
            }
        }
    }

    public void OpenDoor()
    {
        isOpening = true;  // Mulai membuka pintu
    }
}
