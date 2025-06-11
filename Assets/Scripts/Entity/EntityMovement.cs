using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D Rigidbody { get; private set; }
    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();
}
