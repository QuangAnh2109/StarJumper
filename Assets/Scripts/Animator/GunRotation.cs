using UnityEngine;

[RequireComponent(typeof(BaseInputProvider))]
public class GunRotation : MonoBehaviour
{
    private SpriteRenderer gunSprite;
    private BaseInputProvider inputProvider;

    void Awake()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        inputProvider = GetComponent<BaseInputProvider>();
    }

    void Update()
    {
        Vector3 lookDirection = inputProvider.MousePosition - transform.position;

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