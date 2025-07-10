using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class DestroyController : MonoBehaviour
{
    [SerializeField] protected float destroyDelay = 0f;
    private void Start()
    {
        HealthSystem health = GetComponent<HealthSystem>();
        health.OnDeath += SafeDestroy;
    }

    private void SafeDestroy()
    {
        Destroy(gameObject, destroyDelay);
    }
}
