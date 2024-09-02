using UnityEngine;

public class DialogActivator : MonoBehaviour, Interactable
{
    [SerializeField]
    private DialogObject dialogObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.interactable is DialogActivator dialogActivator && dialogActivator == this)
            {
                player.interactable = null;
            }
        }
    }

    public void Interact(Player player)
    {
        player.DialogUI.ShowDialog(dialogObject);
    }
}
