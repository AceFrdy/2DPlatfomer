using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = true;
    private Damageable damageable;
    Animator animator;
    Rigidbody2D rb;
    private bool _isMoving = false;

    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Subscribe ke event OnDeath dari Damageable
        if (damageable != null)
        {
            damageable.OnDeath.AddListener(OnBossDeath);
        }
    }

    public void LookAtPlayer()
    {
        if (player == null) return;

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void ReceiveDamage(int damage, Vector2 knockback)
    {
        if (damageable != null)
        {
            damageable.Hit(damage, knockback);
        }
        else
        {
            Debug.LogWarning("Damageable component not found on Boss!");
        }
    }

    public void OnPlayerDestroyed()
    {
        player = null;
        Debug.Log("Player has been destroyed, stopping LookAtPlayer behavior.");
    }

    private void OnBossDeath()
    {
        // Ketika boss mati, mulai coroutine untuk memberikan jeda sebelum pindah scene
        StartCoroutine(BossDeathRoutine());
    }

    private IEnumerator BossDeathRoutine()
    {
        // Berikan jeda sekitar 2 detik
        yield return new WaitForSeconds(2f);

        // Pindah ke scene baru setelah jeda
        SceneManager.LoadScene("Epilog");
        SoundManager.Instance.StopSound();
        MusicManager.Instance.StopMusic();
    }
}
