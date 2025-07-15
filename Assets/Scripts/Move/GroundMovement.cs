using UnityEngine;

public class GroundMovement : EntityMovement
{
    [HideInInspector] public Vector2 currentVelocity;
    private Vector2 lastPosition;
    private Vector2 newPosition;

    private void Start()
    {
        lastPosition = transform.position;
        newPosition = transform.position;
    }
    protected override void Move()
    {
        newPosition = Vector2.MoveTowards(transform.position, inputProvider.MoveInput, moveSpeed * Time.fixedDeltaTime);
        Rigidbody.MovePosition(newPosition);

        currentVelocity = (newPosition - lastPosition) / Time.fixedDeltaTime;
        lastPosition = newPosition;

    }

}