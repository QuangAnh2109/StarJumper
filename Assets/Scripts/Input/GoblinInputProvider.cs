using UnityEngine;

public class GoblinInputProvider : BaseInputProvider
{
    [Header("AI Settings")]
    [Tooltip("Khoảng cách để AI phát hiện tường phía trước.")]
    [SerializeField] private float wallCheckDistance = 0.5f;

    [Tooltip("Khoảng cách để AI phát hiện mép vực.")]
    [SerializeField] private float groundCheckDistance = 1f;

    [Tooltip("Layer của các đối tượng được coi là 'đất' để AI đi lên.")]
    [SerializeField] private LayerMask groundLayer;

    // Biến nội bộ để lưu hướng di chuyển hiện tại: 1 là sang phải, -1 là sang trái.
    private float moveDirection = 1f;

    // "Bộ não" của AI, được gọi mỗi frame nhờ lớp BaseInputProvider
    protected override void UpdateInput()
    {
        CheckEnvironment();

        MoveInput = new Vector2(moveDirection, 0);

        JumpInput = false;
        AttackInput1 = false;
    }

    private void CheckEnvironment()
    {
        Vector2 raycastOrigin = transform.position;

        Vector2 direction = new Vector2(moveDirection, 0);

        bool isHittingWall = Physics2D.Raycast(raycastOrigin, direction, wallCheckDistance, groundLayer);

        Vector2 groundCheckOrigin = raycastOrigin + direction * 0.5f;
        bool isGroundAhead = Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundLayer);

        Debug.Log($"Hitting Wall: {isHittingWall}, Ground Ahead: {isGroundAhead}");

        if (isHittingWall || !isGroundAhead)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        moveDirection *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = new Vector2(moveDirection, 0);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * wallCheckDistance);

        Vector2 groundCheckOrigin = (Vector2)transform.position + direction * 0.5f;
        Gizmos.DrawLine(groundCheckOrigin, groundCheckOrigin + Vector2.down * groundCheckDistance);
    }
}