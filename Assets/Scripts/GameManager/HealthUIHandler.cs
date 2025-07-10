using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerHealthSystem))]
public class HealthUIHandler : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement healthBarMask;
    private Label healthLabel;
    private PlayerHealthSystem healthSystem;

    private void Awake()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;
            healthBarMask = root.Q<VisualElement>("HealthBarMask");
            healthLabel = root.Q<Label>("HealthLabel");
        }
    }

    private void Start()
    {
        healthSystem = GetComponent<PlayerHealthSystem>();
        UpdateHealthUI();
        healthSystem.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        Debug.Log($"Current Health: {healthSystem.CurrentHealth}, Max Health: {healthSystem.GetMaxHealth()}");
        int currentHealth = healthSystem.CurrentHealth;
        int maxHealth = healthSystem.GetMaxHealth();

        if (healthBarMask != null)
        {
            float percent = Mathf.Clamp01((float)currentHealth / maxHealth);
            healthBarMask.style.width = Length.Percent(percent * 100f);
        }

        if (healthLabel != null)
        {
            healthLabel.text = $"{currentHealth}/{maxHealth}";
        }
    }
}
