using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [HideInInspector] public int CurrentHealth { get; protected set; }
    [HideInInspector] protected bool IsDead => CurrentHealth <= 0;

    [HideInInspector] public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void Update()
    {
        if (IsDead)
        {
            Death();
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}
