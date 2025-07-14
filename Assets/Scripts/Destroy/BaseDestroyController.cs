using System;
using UnityEngine;

public class BaseDestroyController : MonoBehaviour
{
    [SerializeField] protected float destroyDelay = 0f;

    [HideInInspector] public event Action DestroyAction;
    private void OnDestroy()
    {
        DestroyAction?.Invoke();
    }
}