using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonSoundHandler : MonoBehaviour, IPointerEnterHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        // Tambahkan listener untuk onClick
        button.onClick.AddListener(PlayClickSound);
    }

    // Fungsi untuk memutar suara ketika button di-klik
    private void PlayClickSound()
    {
        SoundManager.Instance.PlayClickSound();
    }

    // Fungsi untuk memutar suara ketika pointer di-hover di atas button
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayHoverSound();
    }
}
