using UnityEngine;
public class PlayerInputProvider : BaseInputProvider
{
    protected override void UpdateInput()
    {
        MoveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        JumpInput = Input.GetButtonDown("Jump");
    }
}