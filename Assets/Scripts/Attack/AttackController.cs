using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] public bool canAttack = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f; // Thời gian chờ giữa các lần bắn
    private float nextFireTime = 0f; // Thời điểm có thể bắn tiếp theo

    [Header("Animator Settings")]
    [SerializeField] private Animator animator; // Tham chiếu đến Animator
    [SerializeField] private string shootAnimationTrigger = "Shoot"; // Tên của trigger trong Animator

    [Header("Gizmo Settings")]
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float gizmoArrowLength = 0.5f;

    void OnDrawGizmos()
    {
        if (firePoint == null)
        {
            return;
        }

        Gizmos.color = gizmoColor;

        // Vẽ tia chính
        Gizmos.DrawRay(firePoint.position, firePoint.right * gizmoArrowLength);

        // Vẽ mũi tên phụ
        Vector3 arrowTip = firePoint.position + firePoint.right * gizmoArrowLength;
        Vector3 leftArrow = Quaternion.Euler(0, 0, 30) * (-firePoint.right) * (gizmoArrowLength * 0.2f);
        Vector3 rightArrow = Quaternion.Euler(0, 0, -30) * (-firePoint.right) * (gizmoArrowLength * 0.2f);

        Gizmos.DrawRay(arrowTip, leftArrow);
        Gizmos.DrawRay(arrowTip, rightArrow);
    }

    void Update()
    {
        // Debug.Log($"Can attack: {canAttack}"); // Có thể bỏ comment nếu cần debug

        // Kiểm tra xem người chơi có đang giữ nút chuột phải và đã đến lúc bắn chưa
        // Input.GetMouseButton(1) kiểm tra khi nút chuột phải đang được giữ
        if (Input.GetMouseButton(1) && canAttack && Time.time >= nextFireTime)
        {
            // Debug.Log("Right mouse button held, shooting bullet."); // Có thể bỏ comment nếu cần debug
            Shoot();
            // Cập nhật thời điểm có thể bắn tiếp theo
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet Prefab is not assigned!");
            return;
        }
        if (firePoint == null)
        {
            Debug.LogWarning("Fire Point is not assigned!");
            return;
        }

        // Kích hoạt animation bắn
        if (animator != null)
        {
            animator.SetTrigger(shootAnimationTrigger);
        }

        // Tạo viên đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Có thể thêm logic xử lý viên đạn ở đây, ví dụ:
        // bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * 10f;
    }
}