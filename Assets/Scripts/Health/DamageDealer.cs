using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] protected List<string> enemyTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyTag.Contains(other.gameObject.tag))
        {
            Debug.Log($"DamageDealer dealing {damage} damage to: {other.gameObject.name}");
            IDamageable target = other.GetComponent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
