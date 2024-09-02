// using System.Collections.Generic;
// using UnityEngine;

// public class Player : MonoBehaviour 
// {
//     [SerializeField] private DialogUI dialogUI;
//     private const float MoveSpeed = 10f;
//     public DialogUI DialogUI => dialogUI; // Tambahkan titik koma di akhir baris ini
//     public Interactable Interact { get; set; }

//     private Rigidbody2D rb;

//     private void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     private void Update()
//     {
//         if (dialogUI.IsOpen) return;
//         // Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

//         // rb.MovePosition(rb.position + input.normalized * (MoveSpeed * Time.fixedDeltaTime));

//         if (Input.GetKeyDown(KeyCode.E)) // Perbaiki kesalahan pada baris ini
//         {
//             Interact?.Interact(this);
//         }
//     }
// }
