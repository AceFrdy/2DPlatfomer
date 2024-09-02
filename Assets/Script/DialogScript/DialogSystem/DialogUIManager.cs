// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Playables;

// public class DialogUIManager : MonoBehaviour
// {
//     public Text dialogText;
//     public PlayableDirector playableDirector;
//     public float typingSpeed = 0.05f;

//     private TypewriterEffect typewriterEffect;

//     void Start()
//     {
//         // Mendapatkan komponen TypewriterEffect dari GameObject
//         typewriterEffect = dialogText.GetComponent<TypewriterEffect>();
//         if (typewriterEffect == null)
//         {
//             Debug.LogError("TypewriterEffect component not found on dialogText.");
//         }
//     }

//     public void SetDialog(string dialog)
//     {
//         // Mulai mengetik dialog
//         typewriterEffect.SetText(dialog, typingSpeed);
//     }

//     public void OnTimelinePlay()
//     {
//         // Menghubungkan metode dengan PlayableDirector untuk memulai dialog
//         playableDirector.played += OnPlayableDirectorPlayed;
//     }

//     private void OnPlayableDirectorPlayed(PlayableDirector director)
//     {
//         // Trigger untuk memulai dialog ketika timeline diputar
//         SetDialog("Hello, this is a dialog triggered by the timeline.");
//     }

//     private void OnDestroy()
//     {
//         // Menghapus event listener saat objek dihancurkan
//         playableDirector.played -= OnPlayableDirectorPlayed;
//     }
// }
