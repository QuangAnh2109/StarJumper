using UnityEngine;
public class BulletAIInputProvider : BaseInputProvider
{
    public override void UpdateInput()
    {
        MoveInput = transform.right;
    }
}