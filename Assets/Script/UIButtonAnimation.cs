using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSpriteChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image; // Komponen Image yang akan diubah spritenya
    [SerializeField] private Sprite _normalSprite; // Sprite untuk state normal
    [SerializeField] private Sprite _hoverSprite; // Sprite untuk state hover
    [SerializeField] private Sprite _clickedSprite; // Sprite untuk state clicked

    private void Start()
    {
        // Pastikan sprite awal adalah sprite normal
        _image.sprite = _normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Mengubah sprite ke hover saat pointer masuk
        _image.sprite = _hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Mengembalikan sprite ke normal saat pointer keluar
        _image.sprite = _normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Mengubah sprite ke clicked saat button ditekan
        _image.sprite = _clickedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Mengembalikan sprite ke hover saat pointer masih di atas button setelah button dilepas
        _image.sprite = _hoverSprite;
    }
}
