using UnityEngine;

public abstract class BaseInputProvider : MonoBehaviour
{
    [HideInInspector] public Vector2 MoveInput { get; protected set; }
    [HideInInspector] public bool JumpInput { get; protected set; }
    [HideInInspector] public bool AttackInput { get; protected set; }

    public abstract void UpdateInput();
}