using UnityEngine;

public abstract class BaseInputProvider : MonoBehaviour
{

    [SerializeField] public bool canUpdateInput = true;
    [HideInInspector] public Vector2 MoveInput { get; protected set; }
    [HideInInspector] public bool JumpInput { get; protected set; }
    [HideInInspector] public bool AttackInput1 { get; protected set; }
    [HideInInspector] public Vector3 MousePosition { get; protected set; }

    private void Update()
    {
        if (canUpdateInput)
        {
            UpdateInput();
        }
    }

    protected abstract void UpdateInput();
}