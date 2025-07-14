using UnityEngine;
public class BulletAIInputProvider : BaseInputProvider
{
    protected override void UpdateInput()
    {
        MoveInput = transform.right;
    }
}