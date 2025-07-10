using SavedSettings;
using UnityEngine;

public class BulletMovement : EntityMovement
{
    private void Update()
    {
        inputProvider.UpdateInput();
    }
    protected override void Move()
    {
        Rigidbody.MovePosition(Rigidbody.position + moveSpeed * Time.fixedDeltaTime * inputProvider.MoveInput);
    }
}