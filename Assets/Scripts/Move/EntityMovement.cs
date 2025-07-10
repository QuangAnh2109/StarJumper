using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BaseInputProvider))]
public abstract class EntityMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected BaseInputProvider inputProvider;

    protected Rigidbody2D Rigidbody { get; private set; }
    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        inputProvider = GetComponent<BaseInputProvider>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();
}
