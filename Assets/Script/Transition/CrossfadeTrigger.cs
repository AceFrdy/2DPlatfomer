using UnityEngine;

public class CrossfadeTrigger : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerCrossfade()
    {
        animator.SetTrigger("Start"); // Pastikan nama trigger ini sesuai dengan yang ada di Animator
    }
}
