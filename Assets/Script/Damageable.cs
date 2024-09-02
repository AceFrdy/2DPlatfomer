using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    public UnityEvent OnDeath;
    // public int instantDeathDamage = 1000;

    [SerializeField]
    private int _maxHealth = 200;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 200;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if (Health <= 100)
            {
                Debug.Log("Boss Enraged!");
                IsEnraged = true;
            }

            if (_health <= 0 && IsAlive)
            {
                IsAlive = false;
                HandleDeath();
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool _isEnraged = false;
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            SoundManager.Instance.PlaySound2D("DeathSFX");
        }
    }
    public bool IsEnraged
    {
        get { return _isEnraged; }
        set
        {
            _isEnraged = value;
            animator.SetBool("isEnraged", value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        // // Tambahkan kondisi ini di Update
        // if (Health <= 0 && !IsAlive)
        // {
        //     IsAlive = false; // Set IsAlive to false
        //     gameManager.gameOver(); // Panggil Game Over dari GameManager
        // }
    }
    public bool TakeDamage(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true; // Set invincibility on hit
            timeSinceHit = 0; // Reset time since last hit

            Debug.Log("HealthBoss: " + Health);

            // Trigger hit animation and knockback
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = false;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            // Check if boss should become enraged
            if (gameObject.CompareTag("Boss") || GetComponent<BossController>() != null)
        {
            // Cek jika boss harus menjadi enraged
            if (!IsEnraged && Health <= MaxHealth * 0.5f)
            {
                IsEnraged = true; // Aktifkan boolean isEnraged
                Debug.Log("Boss Enraged at 50% HP!");
            }
        }

            // Handle boss death
            if (Health <= 0)
            {
                HandleDeath();
            }

            return true;
        }
        return false;
    }


    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = false;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }

    private void HandleDeath()
    {
        OnDeath?.Invoke();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
