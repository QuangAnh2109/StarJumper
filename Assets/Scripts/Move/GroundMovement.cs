using UnityEngine;

public class GroundMovement : EntityMovement
{
    protected override void Move()
    {
        Rigidbody.MovePosition(Vector2.MoveTowards(transform.position, inputProvider.MoveInput, moveSpeed * Time.fixedDeltaTime));
    }
}