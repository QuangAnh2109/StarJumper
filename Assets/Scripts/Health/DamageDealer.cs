using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] protected List<string> enemyTags;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyTags.Contains(other.gameObject.tag))
        {
            IDamageable target = other.GetComponent<IDamageable>();

            if (target != null)
            {
                Debug.Log($"DamageDealer dealing {damage} damage to: {other.gameObject.name}");
                target.TakeDamage(damage);
            }
        }
    }
}
