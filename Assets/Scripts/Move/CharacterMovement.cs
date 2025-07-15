using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : EntityMovement
{
    [SerializeField] protected float jumpForce = 7f;
    [SerializeField] protected int maxJumpCount = 1;
    [SerializeField] protected Transform groundCheckPoint;
    [SerializeField] protected Vector2 groundCheckSize1 = new Vector2(0.55f, 0.2f);
    [SerializeField] protected Vector2 groundCheckSize2 = new Vector2(0.55f, 0.01f);
    [SerializeField] protected List<LayerMask> groundLayer;

    protected Animator animator;
    protected bool isGround;
    protected bool wasGrounded;
    protected int jumpCount;
    protected float moveInputHorizontal;
    protected bool isJumping;
    protected bool isRunning;
    protected SpriteRenderer spriteRenderer;
    protected GroundMovement currentPlatform;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize1);
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize2);
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        isGround = false;
        wasGrounded = false;
        jumpCount = 0;
        moveInputHorizontal = 0;
        isJumping = false;
    }

    protected virtual void Update()
    {
        GetInput();
        if (animator != null)
        {
            UpdateAnimator();
        }
    }

    protected virtual void UpdateAnimator()
    {
        animator.SetBool("isGrounded", GroundCheck(groundCheckSize2));
        animator.SetBool("isRunning", isRunning);
        if (!animator.GetBool("isGrounded"))
        {
            if (Rigidbody.linearVelocity.y > 0.1f)
            {
                animator.SetBool("isJumping", true);
            }
            else animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", isJumping);
        }
    }

    private void GetInput()
    {
        moveInputHorizontal = inputProvider.MoveInput.x;
        if (!isJumping)
        {
            isJumping = inputProvider.JumpInput;
        }
    }

    protected override void Move()
    {
        isGround = GroundCheck(groundCheckSize1);
        ResetJumpCount();
        MovementHorizontal();
        Jump();
    }

    protected virtual void MovementHorizontal()
    {
        Rigidbody.linearVelocity = new Vector2(moveInputHorizontal * moveSpeed, Rigidbody.linearVelocity.y);
        if (currentPlatform != null)
        {
            Rigidbody.linearVelocity += new Vector2(currentPlatform.currentVelocity.x, 0);
            if(currentPlatform.currentVelocity.y <= 0)
            {
                Rigidbody.linearVelocity += new Vector2(0, currentPlatform.currentVelocity.y);
            }
        }
        isRunning = moveInputHorizontal != 0;
        if (moveInputHorizontal > 0) GetComponent<SpriteRenderer>().flipX = false;
        else if (moveInputHorizontal < 0) GetComponent<SpriteRenderer>().flipX = true;
    }

    protected virtual void Jump()
    {
        if (isJumping)
        {
            if (JumpCheck())
            {
                Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, jumpForce);
                jumpCount++;
            }
            isJumping = false;
        }
    }

    protected virtual bool GroundCheck(Vector2 groundCheckSize)
    {
        foreach (LayerMask layer in groundLayer)
        {
            Collider2D collider = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, layer);
            if (collider != null)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void ResetJumpCount()
    {
        if (isGround && !wasGrounded)
        {
            jumpCount = 0;
        }
        wasGrounded = isGround;
    }

    protected virtual bool JumpCheck()
    {
        return (isGround || (jumpCount < maxJumpCount && jumpCount != 0));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var platform = collider.GetComponent<GroundMovement>();
        if (platform != null)
        {
            currentPlatform = platform;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<GroundMovement>() == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}