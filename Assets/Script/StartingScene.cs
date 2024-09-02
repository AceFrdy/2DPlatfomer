using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public static bool isCutsceneOn;
    public Animator camAnim;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // playerWalking.Speed = 0;
            isCutsceneOn = true;
            camAnim.SetBool("CutsceneBoss", true);
            Invoke(nameof(StopCutScene), 3f);
        }
    }
    void StopCutScene()
    {
        // playerWalking.Speed = 0;
        isCutsceneOn = false;
        camAnim.SetBool("CutsceneBoss", false);
        Destroy(gameObject);
    }
}
