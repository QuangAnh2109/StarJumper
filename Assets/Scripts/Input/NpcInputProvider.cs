using UnityEngine;

public class NpcInputProvider : BaseInputProvider
{
    [Header("AI Patrol Settings")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float wallCheckYOffset = -0.2f;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("AI Attack Settings")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform attackPoint;

    private int moveDirection = 1;

    protected override void UpdateInput()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, attackRange, targetLayer);

        if (target != null)
        {
            HandleAttackState(target);
        }
        else
        {
            HandlePatrolState();
        }
    }

    private void HandleAttackState(Collider2D target)
    {
        MoveInput = Vector2.zero;
        AttackInput1 = true;

        float directionToTarget = target.transform.position.x - transform.position.x;
        moveDirection = (int)Mathf.Sign(directionToTarget);

        float angle = (moveDirection == 1) ? 0 : 180;
        attackPoint.rotation = Quaternion.Euler(attackPoint.rotation.eulerAngles.x, angle, attackPoint.rotation.eulerAngles.z);
    }

    private void HandlePatrolState()
    {
        AttackInput1 = false;
        CheckPatrolEnvironment();
        MoveInput = new Vector2(moveDirection, 0);
    }

    private void CheckPatrolEnvironment()
    {
        Vector2 raycastOrigin = (Vector2)transform.position + new Vector2(0, wallCheckYOffset);
        Vector2 direction = new Vector2(moveDirection, 0);
        bool isHittingWall = Physics2D.Raycast(raycastOrigin, direction, wallCheckDistance, groundLayer);

        Vector2 groundCheckOrigin = (Vector2)transform.position + direction * 0.5f;
        bool isGroundAhead = Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);

        if (isHittingWall || !isGroundAhead)
        {
            moveDirection *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Vector2 direction = new Vector2(moveDirection, 0);

        Vector2 wallRayOrigin = (Vector2)transform.position + new Vector2(0, wallCheckYOffset);
        Gizmos.DrawLine(wallRayOrigin, wallRayOrigin + direction * wallCheckDistance);

        Vector2 groundCheckOrigin = (Vector2)transform.position + direction * 0.5f;
        Gizmos.DrawLine(groundCheckOrigin, groundCheckOrigin + Vector2.down * groundCheckDistance);
    }
}