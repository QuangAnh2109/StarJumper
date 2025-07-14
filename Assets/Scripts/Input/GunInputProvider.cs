
using UnityEngine;

internal class GunInputProvider : BaseInputProvider
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    protected override void UpdateInput()
    {
        AttackInput = Input.GetMouseButton(0);

        if (mainCamera != null)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            mouseScreenPosition.z = transform.position.z - mainCamera.transform.position.z;

            MousePosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        }
    }
}
