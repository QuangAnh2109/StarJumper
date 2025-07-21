using UnityEngine;

public class HealthTest : MonoBehaviour
{
    private PlayerHealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();
        if (healthSystem == null)
        {
            Debug.LogError("Không tìm thấy PlayerHealthSystem trên GameObject này!");
        }
    }

    private void Update()
    {
        // Nhấn phím Space để giảm 10 máu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.TakeDamage(10);
        }
    }
}
