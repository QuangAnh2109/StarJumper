using UnityEngine;

[RequireComponent(typeof(BaseInputProvider))]
public class AttackController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private string shootAnimationTrigger = "Shoot";
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float gizmoArrowLength = 0.5f;

    private float nextFireTime = 0f;
    private BaseInputProvider inputProvider;
    private Animator animator;

    void OnDrawGizmos()
    {
        if (firePoint == null)
        {
            return;
        }

        Gizmos.color = gizmoColor;

        Gizmos.DrawRay(firePoint.position, firePoint.right * gizmoArrowLength);

        Vector3 arrowTip = firePoint.position + firePoint.right * gizmoArrowLength;
        Vector3 leftArrow = Quaternion.Euler(0, 0, 30) * (-firePoint.right) * (gizmoArrowLength * 0.2f);
        Vector3 rightArrow = Quaternion.Euler(0, 0, -30) * (-firePoint.right) * (gizmoArrowLength * 0.2f);

        Gizmos.DrawRay(arrowTip, leftArrow);
        Gizmos.DrawRay(arrowTip, rightArrow);
    }

    void Awake()
    {
        inputProvider = GetComponent<BaseInputProvider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (inputProvider.AttackInput && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.Log("Bullet prefab or fire point is not set.");
            return;
        }

        if (animator != null)
        {
            animator.SetTrigger(shootAnimationTrigger);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}