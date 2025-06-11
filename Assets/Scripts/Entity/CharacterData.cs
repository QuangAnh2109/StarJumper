using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : EntityData
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    // Initialize current health to max health at the start
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }


    // Add health to the current health, ensuring it does not exceed max health
    public void AddHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Subtract health from the current health, ensuring it does not go below zero
    public void SubtractHealth(int health)
    {
        currentHealth -= health;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
