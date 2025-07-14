using UnityEngine;

public abstract class BaseInputProvider : MonoBehaviour
{
    [HideInInspector] public Vector2 MoveInput { get; protected set; }
    [HideInInspector] public bool JumpInput { get; protected set; }
    [HideInInspector] public bool AttackInput { get; protected set; }
    [HideInInspector] public Vector3 MousePosition { get; protected set; }
    [SerializeField] public bool canUpdateInput = true;

    private void Update()
    {
        if (canUpdateInput)
        {
            UpdateInput();
        }
    }

    protected abstract void UpdateInput();
}