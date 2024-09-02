// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public abstract class State : MonoBehaviour {
//     protected Core core;
//     protected Rigidbody2D body => core.body;
//     protected Animator animator => core.animator;

//     public virtual void Enter() { }
//     public virtual void Do() { }
//     public virtual void FixedDo() { }
//     public virtual void Exit() { }

//     public void SetCore(Core _core) {
//         core = _core;
//     }
//     public void Initialise() {
//         isComplete = false;
//         startTime = Time.time;
//     }
// }
