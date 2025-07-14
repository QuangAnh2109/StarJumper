using UnityEngine;

public class GoblinInputProvider : BaseInputProvider
{
    #region State Machine
    // Định nghĩa các trạng thái của AI
    private enum State { Patrolling, Chasing, Attacking }
    private State currentState;
    #endregion

    #region Inspector Variables
    [Header("Dependencies")]
    [SerializeField] private Animator anim; // Cần Animator để kích hoạt trigger tấn công

    [Header("Patrolling Settings")]
    [SerializeField] private Transform checkLedge;
    [SerializeField] private Transform checkWall;
    [SerializeField] private LayerMask whatIsGround;
    private bool facingRight = true;

    [Header("Chase & Attack Settings")]
    [SerializeField] private float playerCheckDistance = 8f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private LayerMask whatIsPlayer;
    private float nextAttackTime = 0f;

    private Transform player; // Tham chiếu đến người chơi
    #endregion

    private void Start()
    {
        // Khởi tạo trạng thái ban đầu
        currentState = State.Patrolling;
        // Tìm người chơi trong scene bằng tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    protected override void UpdateInput()
    {
        // Reset các giá trị input ở đầu mỗi frame
        MoveInput = Vector2.zero;
        AttackInput = false;

        if (player == null)
        {
            // Nếu không tìm thấy người chơi, chỉ đi tuần tra
            currentState = State.Patrolling;
        }

        // --- Máy Trạng Thái (State Machine) ---
        switch (currentState)
        {
            case State.Patrolling:
                HandlePatrollingState();
                break;
            case State.Chasing:
                HandleChasingState();
                break;
            case State.Attacking:
                HandleAttackingState();
                break;
        }
    }

    private void HandlePatrollingState()
    {
        // Logic tuần tra: di chuyển theo hướng hiện tại
        MoveInput = new Vector2(facingRight ? 1 : -1, 0);

        // Kiểm tra điều kiện để lật hướng
        bool wallInFront = Physics2D.OverlapCircle(checkWall.position, 0.1f, whatIsGround);
        bool ledgeInFront = !Physics2D.OverlapCircle(checkLedge.position, 0.1f, whatIsGround);
        Debug.Log($"Wall: {wallInFront}, Ledge: {ledgeInFront}");
        if (wallInFront || ledgeInFront)
        {
            Flip();
        }

        // Kiểm tra điều kiện để chuyển sang trạng thái Rượt đuổi
        if (IsPlayerInSight())
        {
            currentState = State.Chasing;
        }
    }

    private void HandleChasingState()
    {
        // Logic rượt đuổi: di chuyển về phía người chơi
        if (player.position.x > transform.position.x)
        {
            if (!facingRight) Flip();
            MoveInput = Vector2.right;
        }
        else
        {
            if (facingRight) Flip();
            MoveInput = Vector2.left;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Kiểm tra điều kiện để chuyển sang trạng thái Tấn công
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            currentState = State.Attacking;
        }
        // Kiểm tra điều kiện để quay lại Tuần tra (nếu người chơi quá xa)
        else if (distanceToPlayer > playerCheckDistance * 1.2f)
        {
            currentState = State.Patrolling;
        }
    }

    private void HandleAttackingState()
    {
        // Logic tấn công: dừng di chuyển và đặt AttackInput thành true
        MoveInput = Vector2.zero;
        AttackInput = true;

        // Kích hoạt animation (nếu có)
        if (anim != null) anim.SetTrigger("Attack");

        // Đặt thời gian hồi chiêu
        nextAttackTime = Time.time + attackCooldown;

        // Sau khi tấn công, ngay lập tức quay lại trạng thái rượt đuổi để đánh giá lại tình hình
        currentState = State.Chasing;
    }

    #region Helper Methods
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private bool IsPlayerInSight()
    {
        if (player == null) return false;
        return Vector2.Distance(transform.position, player.position) < playerCheckDistance;
    }

    // (Tùy chọn) Vẽ các gizmo để dễ debug trong Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // --- Vẽ vùng kiểm tra tường (Màu xanh dương) ---
        if (checkWall != null)
        {
            Gizmos.color = Color.blue;
            // Bán kính 0.1f này PHẢI khớp với bán kính bạn dùng trong hàm Physics2D.OverlapCircle
            Gizmos.DrawWireSphere(checkWall.position, 0.1f);
        }

        // --- Vẽ vùng kiểm tra vực (Màu xanh lá) ---
        if (checkLedge != null)
        {
            Gizmos.color = Color.green;
            // Bán kính này cũng PHẢI khớp với trong code logic
            Gizmos.DrawWireSphere(checkLedge.position, 0.1f);
        }
    }
    #endregion
}