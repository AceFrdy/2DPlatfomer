// using UnityEngine;
// using UnityEngine.Playables;
// using System.Collections;

// public class DialogCutsceneController : MonoBehaviour
// {
//     public PlayableDirector director;
//     public DialogObject dialogObject; // Deklarasikan DialogObject di sini
//     public DialogCutsceneUI dialogCutsceneUI; // Komponen UI untuk menampilkan dialog

//     private void Start()
//     {
//         director.Freeze();
        
//         // Pastikan dialogObject telah diinisialisasi
//         if (dialogObject != null && dialogCutsceneUI != null)
//         {
//             dialogCutsceneUI.ShowDialog(dialogObject);
//         }
        
//         StartCoroutine(ResumeTimelineAfterDelay(3f));
//     }

//     private IEnumerator ResumeTimelineAfterDelay(float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         director.Unfreeze();
//     }
// }
