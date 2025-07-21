using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealthSystem))]
public class HealthUIHandler : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    [SerializeField] private Text healthText;

    private PlayerHealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();

        if (healthSystem == null)
        {
            Debug.LogError("Không tìm thấy PlayerHealthSystem trên GameObject này!");
            enabled = false;
            return;
        }

        // Cập nhật UI lần đầu
        UpdateHealthUI();

        // Đăng ký sự kiện thay đổi máu
        healthSystem.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh lỗi khi object bị hủy
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateHealthUI;
        }
    }

    private void UpdateHealthUI()
    {
        int currentHealth = healthSystem.CurrentHealth;
        int maxHealth = healthSystem.GetMaxHealth();
        float percent = Mathf.Clamp01((float)currentHealth / maxHealth);

        Debug.Log($"UpdateHealthUI called - Health: {currentHealth}/{maxHealth} ({percent * 100}%)");

        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = percent;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }
}
