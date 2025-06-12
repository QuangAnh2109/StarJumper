using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : EntityData
{
    [SerializeField]
    private int maxHealth = 100;
    public int CurrentHealth { get; private set; }

    // Initialize current health to max health at the start
    protected override void OnEnable()
    {
        base.OnEnable();
        CurrentHealth = maxHealth;
    }

    // Add health to the current health, ensuring it does not exceed max health
    public void AddHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }

    // Subtract health from the current health, ensuring it does not go below zero
    public void SubtractHealth(int health)
    {
        CurrentHealth -= health;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
