using UnityEngine;
using System;

public class PlayerHealthSystem : HealthSystem, IDamageable
{
    [HideInInspector] public event Action OnHealthChanged;
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {CurrentHealth}");
        OnHealthChanged?.Invoke();
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
