using UnityEngine;

public abstract class CharacterMovement : EntityMovement
{
    [SerializeField] protected float jumpForce = 7f;
    [SerializeField] protected int maxJumpCount = 1;
    [SerializeField] protected Transform groundCheckPoint;
    [SerializeField] protected Vector2 groundCheckSize1 = new Vector2(0.55f, 0.2f);
    [SerializeField] protected Vector2 groundCheckSize2 = new Vector2(0.55f, 0.01f);
    [SerializeField] protected LayerMask groundLayer;

    protected CharacterAnimator characterAnimator;
    protected bool isGround;
    protected bool wasGrounded;
    protected int jumpCount;
    protected float moveInputHorizontal;
    protected bool isJumping;
    protected bool isRunning;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize1);
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize2);
    }

    protected override void Awake()
    {
        base.Awake();
        characterAnimator = GetComponent<CharacterAnimator>();
        if (characterAnimator == null)
        {
            Debug.LogError("EntityAnimator component is missing on " + gameObject.name);
        }
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
        if (characterAnimator != null)
        {
            UpdateAnimator();
        }
    }

    protected virtual void UpdateAnimator()
    {
        characterAnimator.isGrounded = GroundCheck(groundCheckSize2);
        characterAnimator.isRunning = isRunning;
        if (!characterAnimator.isGrounded)
        {
            if (Rigidbody.linearVelocity.y > 0.1f)
            {
                characterAnimator.isJumping = true;
            }
            else characterAnimator.isJumping = false;
        }
        else
        {
            characterAnimator.isJumping = isJumping;
        }
    }

    protected abstract void GetInput();

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
        isRunning = moveInputHorizontal != 0;
        if (isRunning) transform.localScale = new Vector3(Mathf.Sign(moveInputHorizontal), transform.localScale.y, transform.localScale.z);
    }

    protected virtual void Jump()
    {
        if (isJumping)
        {
            if (JumpCheck())
            {

                Rigidbody.linearVelocity = new Vector2(base.Rigidbody.linearVelocity.x, jumpForce);
                jumpCount++;
            }
            isJumping = false;
        }
    }

    protected virtual bool GroundCheck(Vector2 groundCheckSize)
    {
        return Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0f, groundLayer) != null;
    }

    protected virtual void ResetJumpCount()
    {
        if (isGround && !wasGrounded)
        {
            jumpCount = 0;
        }
        wasGrounded = isGround;
    }

    // Checks if the player can jump based on ground state and jump count
    protected virtual bool JumpCheck()
    {
        return (isGround || (jumpCount < maxJumpCount && jumpCount != 0));
    }
}