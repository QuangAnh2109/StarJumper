
using UnityEngine;

internal class GunInputProvider : BaseInputProvider
{
    public override void UpdateInput()
    {
        AttackInput = Input.GetMouseButton(0);
    }
}
