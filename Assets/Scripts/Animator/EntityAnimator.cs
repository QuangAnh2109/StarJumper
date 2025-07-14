using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class EntityAnimator : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        UpdateAnimatorParameters();
    }

    protected abstract void UpdateAnimatorParameters();
}
