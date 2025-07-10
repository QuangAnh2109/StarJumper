using UnityEngine;

public abstract class EntityAnimator : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
    }

    protected virtual void Update()
    {
        UpdateAnimatorParameters();
    }

    protected abstract void UpdateAnimatorParameters();
}
