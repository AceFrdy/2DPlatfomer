// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public abstract class Core : MonoBehaviour {
//     public Rigidbody2D body;
//     public Animator animator;

//     public StateMachine machine;

//     public void SetupInstance() {
//         machine = new StateMachine();

//         State[] allChildStates = GetComponentsInChildrend<State>();
//         foreach (State state in allChildStates) {
//             state.SetCore(this);
//         }
//     }
// }
