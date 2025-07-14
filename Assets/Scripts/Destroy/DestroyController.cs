using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class DestroyController : BaseDestroyController
{
    private void Start()
    {
        HealthSystem health = GetComponent<HealthSystem>();
        health.DeathAction += SafeDestroy;
    }

    private void SafeDestroy()
    {
        Destroy(gameObject, destroyDelay);
    }
}
