// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
// public class Boar : MonoBehaviour
// {

//     public float walkSpeed = 3f;

//     Rigidbody2D rb;
//     TouchingDirections touchingDirections;

//     public enum WalkableDirection {Right, Left}

//     private WalkableDirection _walkDirection;
//     private Vector2 walkDirectionVector = Vector2.left;

//     public WalkableDirection WalkDirection
//     {
//         get { return _walkDirection; }
//         set {
//             if(_walkDirection != value)
//             {
//                 // Direction Fliped
//                 gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
//                 if(value == WalkableDirection.Left)
//                 {
//                     walkDirectionVector = Vector2.left;
//                 } else if (value == WalkableDirection.Right)
//                 {
//                     walkDirectionVector = Vector2.right;
//                 }
//             }
//         _walkDirection = value;}
//     }

//     private void Awake()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         touchingDirections = GetComponent<TouchingDirections>();
//     }

//     private void FixedUpdate()
//     {
//         if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
//         {
//             FlipDirection();
//     }
//         rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
//     }
//     private void FlipDirection()
//     {
//         if(WalkDirection == WalkableDirection.Left)
//         {
//             WalkDirection = WalkableDirection.Right;
//         } else if (WalkDirection == WalkableDirection.Right)
//         {
//             WalkDirection = WalkableDirection.Left;
//         } else 
//         {
//             Debug.LogError("Current walkable direction is set to legal values of right or right");
//         }
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
