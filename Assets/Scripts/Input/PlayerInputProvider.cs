using UnityEngine;
public class PlayerInputProvider : BaseInputProvider
{
    public override void UpdateInput()
    {
        MoveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        JumpInput = Input.GetButtonDown("Jump");
    }
}