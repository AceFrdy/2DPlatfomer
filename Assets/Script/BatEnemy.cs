using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public DetectionZone biteDetectionZone;

    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    public float stopDistance = 1f; // Jarak berhenti

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    public bool _hasTarget = false;

    public List<Transform> patrolPoints;
    // public Collider2D deathCollider;
    public float patrolWaitTime = 2f; // Waktu tunggu di setiap patrol point
    private float patrolWaitTimer;
    private int currentPatrolIndex;

    private GameObject player;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            if (animator != null)
            {
                animator.SetBool(AnimationStrings.hasTarget, value);
            }
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAttacking
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAttacking);
        }
    }
    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0f));
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player object has the tag 'Player'.");
        }
        currentPatrolIndex = 0;
        patrolWaitTimer = patrolWaitTime;
    }

    // private void OnEnable()
    // {
    //     damageable.damageableDeath.AddListener(OnDeath);
    // }

    // private void OnDisable()
    // {
    //     damageable.damageableDeath.RemoveListener(OnDeath);
    // }

    // Update is called once per frame
    void Update()
    {
        if (player == null || biteDetectionZone == null || !damageable.IsAlive)
            return;

        if (chase)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > stopDistance)
            {
                Chase();
            }
            else
            {
                Stop();
            }
        }
        else
        {
            if (patrolPoints.Count > 0)
            {
                Patrol();
            }
            else
            {
                ReturnStartingPoint();
            }
        }

        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove && !IsAttacking)
            {
                if (chase)
                {
                    if (Vector2.Distance(transform.position, player.transform.position) > stopDistance)
                    {
                        Chase();
                    }
                    else
                    {
                        Stop();
                    }
                }
                else
                {
                    if (patrolPoints.Count > 0)
                    {
                        Patrol();
                    }
                    else
                    {
                        ReturnStartingPoint();
                    }
                }
            }
        }
        else
        {
            FallToGround();
        }
    }

    private void Chase()
    {
        if (damageable.IsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Flip(player.transform.position.x);
        }
    }

    private void ReturnStartingPoint()
    {
        if (damageable.IsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
            Flip(startingPoint.position.x);
        }
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private void Flip(float targetPositionX)
    {
        if (transform.position.x > targetPositionX)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Patrol()
    {
        if (damageable.IsAlive)
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
                Flip(patrolPoints[currentPatrolIndex].position.x);
            }
            else
            {
                patrolWaitTimer -= Time.deltaTime;
                if (patrolWaitTimer <= 0f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                    patrolWaitTimer = patrolWaitTime;
                }
            }
        }
    }

    // public void OnDeath()
    // {
    //     // Menghentikan pergerakan dengan mengunci posisi dan rotasi
    //     rb.velocity = Vector2.zero;
    //     rb.constraints = RigidbodyConstraints2D.FreezeAll; // Mengunci posisi dan rotasi
    //     deathCollider.enabled = true;

    //     // Reset rotation to default (facing right)
    //     transform.rotation = Quaternion.Euler(0, 0, 0);
    // }
    private void FallToGround()
    {
        // Menghentikan pergerakan horizontal
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.gravityScale = 2f;

        // Mengecek apakah musuh telah menyentuh tanah menggunakan Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
    }
}
