using UnityEngine;

public class GunRotation : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer gunSprite;

    void Awake()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
        {
            return;
        }

        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.z = transform.position.z - mainCamera.transform.position.z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 lookDirection = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (gunSprite != null)
        {
            if (angle > 90 || angle < -90)
            {
                gunSprite.flipY = true;
            }
            else
            {
                gunSprite.flipY = false;
            }
        }
    }
}