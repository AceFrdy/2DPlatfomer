using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private DialogUI dialogUI;

    public DialogUI DialogUI => dialogUI;
    public Interactable interactable { get; set; }
    public GameManagerScript gameManager;
// private bool isAttacking = false;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    // public float jumpImpulse = 5f;
    public float jumpForce = 10f; // Ganti nilai ini untuk mengatur kekuatan lompatan  
    public float gravity = -9.81f;
    private float jumpTime = 0f;
    private bool isJumping = false;
    public float airDragFactor = 0.98f; // Faktor perlambatan
    private int jumpCount = 0;
    public int maxJumps = 2; // Maksimal jumlah lompatan, bisa diatur menjadi 2 untuk double jump
    // private bool jumpBuffered = false;


    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;
    private float lastGroundedTime;


    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (isRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        // Air state checks
                        return airWalkSpeed;
                    }
                }
                else
                {
                    // idle speed is 0
                    return 0;
                }
            }
            else
            {
                return 0;
            }
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
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                // flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (dialogUI.IsOpen) return; // Stop movement if dialog is open

        if (!damageable.LockVelocity)
        {
            if (isJumping && !touchingDirections.IsGrounded)
            {
                jumpTime += Time.fixedDeltaTime;

                // Izinkan pemain bergerak ke kiri/kanan saat di udara
                float airMoveSpeed = moveInput.x * airWalkSpeed;
                rb.velocity = new Vector2(airMoveSpeed, rb.velocity.y);
            }
            else
            {
                // Reset kecepatan horizontal saat di darat
                isJumping = false;
                rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
            }
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            interactable.Interact(this);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (dialogUI.IsOpen) return; // Stop processing movement input if dialog is open

        moveInput = context.ReadValue<Vector2>();
        if (IsAlive && CanMove)
        {
            isMoving = moveInput != Vector2.zero;
            setFacingDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }
    }


    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public void onRun(InputAction.CallbackContext context)
    {
        if (dialogUI.IsOpen) return; // Stop running if dialog is open

        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (dialogUI.IsOpen) return; // Hentikan lompat jika dialog terbuka

        if (context.started && CanMove)
        {
            if (touchingDirections.IsGrounded)
            {
                // Reset jumpCount saat pemain di tanah
                jumpCount = 0;
            }

            // Cek apakah pemain bisa melompat
            if (jumpCount < maxJumps)
            {
                if (touchingDirections.IsGrounded && jumpCount == 0)
                {
                    animator.SetTrigger(AnimationStrings.jumpTrigger);
                }
                else if (!touchingDirections.IsGrounded && jumpCount == 1)
                {
                    animator.SetTrigger(AnimationStrings.doubleJumpTrigger);
                }

                rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset kecepatan vertikal sebelum menambahkan gaya
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                jumpTime = 0f;
                jumpCount++; // Tambahkan jumpCount setiap kali pemain melompat
            }
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (dialogUI.IsOpen) return; // Stop attacking if dialog is open

        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void onRangedAttack(InputAction.CallbackContext context)
    {
        if (dialogUI.IsOpen) return; // Stop ranged attacking if dialog is open

        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        // Cek apakah karakter masih hidup
        if (damageable.Health <= 0)
        {
            gameManager.gameOver();
        }
    }
}
