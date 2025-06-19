using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : CharacterMovement
{
    protected override void GetInput()
    {
        moveInputHorizontal = Input.GetAxis("Horizontal");
        if(!isJumping) isJumping = Input.GetButtonDown("Jump");
    }
}