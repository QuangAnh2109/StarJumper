using SavedSettings;
using UnityEngine;

public class BulletMovement : EntityMovement
{
    protected override void Move()
    {
        Rigidbody.MovePosition(Rigidbody.position + moveSpeed * Time.fixedDeltaTime * inputProvider.MoveInput);
    }
}