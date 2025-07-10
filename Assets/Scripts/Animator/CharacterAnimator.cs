using UnityEngine;

public class CharacterAnimator : EntityAnimator
{
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isGrounded;

    protected virtual void Start()
    {
        isRunning = false;
        isJumping = false;
        isGrounded = false;
    }

    protected override void UpdateAnimatorParameters()
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);
    }
}