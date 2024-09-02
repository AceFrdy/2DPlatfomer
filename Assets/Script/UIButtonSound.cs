using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayHoverSound();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayClickSound();
    }
}
