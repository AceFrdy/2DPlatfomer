using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class NPCTest : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    public List<Transform> patrolPoints;
    public float patrolWaitTime = 2f;
    public Transform startingPoint; // Tambahkan starting point

    private Rigidbody2D rb;
    private TouchingDirections touchingDirections;
    private Animator animator;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.left;
    private int currentPatrolIndex;
    private float patrolWaitTimer;
    private bool waiting = false;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Direction Flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                walkDirectionVector = (value == WalkableDirection.Left) ? Vector2.left : Vector2.right;
            }
            _walkDirection = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            if (_isMoving != value)
            {
                _isMoving = value;
                animator.SetBool(AnimationStrings.isMoving, value);
                Debug.Log("isMoving set to: " + value); // Tambahkan log untuk memastikan perubahan nilai
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        currentPatrolIndex = 0;
        patrolWaitTimer = patrolWaitTime;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (CanMove && touchingDirections.IsGrounded)
        {
            if (waiting)
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.1f; // Pastikan untuk memperhitungkan pergerakan kecil
    }

    private void Patrol()
    {
        if (patrolPoints.Count > 0)
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) > 0.1f)
            {
                Vector2 direction = ((Vector2)patrolPoints[currentPatrolIndex].position - rb.position).normalized;
                rb.velocity = new Vector2(direction.x * maxSpeed, rb.velocity.y);
                Flip(patrolPoints[currentPatrolIndex].position.x);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                StartCoroutine(WaitAtPatrolPoint());
            }
        }
        else
        {
            ReturnToStartingPoint();
        }
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        waiting = true;
        isMoving = false;
        yield return new WaitForSeconds(patrolWaitTime);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        isMoving = true;
        waiting = false;
    }

    private void ReturnToStartingPoint()
    {
        if (Vector2.Distance(transform.position, startingPoint.position) > 0.1f)
        {
            Vector2 direction = ((Vector2)startingPoint.position - rb.position).normalized;
            rb.velocity = new Vector2(direction.x * maxSpeed, rb.velocity.y);
            Flip(startingPoint.position.x);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        WalkDirection = (WalkDirection == WalkableDirection.Left) ? WalkableDirection.Right : WalkableDirection.Left;
    }

    private void Flip(float targetPositionX)
    {
        bool shouldFlip = (targetPositionX < transform.position.x && WalkDirection == WalkableDirection.Right) ||
                          (targetPositionX > transform.position.x && WalkDirection == WalkableDirection.Left);
        if (shouldFlip)
        {
            WalkDirection = (WalkDirection == WalkableDirection.Left) ? WalkableDirection.Right : WalkableDirection.Left;
        }
    }
}
