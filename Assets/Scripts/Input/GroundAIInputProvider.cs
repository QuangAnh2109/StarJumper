using UnityEngine;
public class GroundAIInputProvider : BaseInputProvider
{

    [SerializeField] protected Vector2 endPointLeft;
    [SerializeField] protected Vector2 endPointRight;

    protected Vector2 currentPoint;
    protected Vector2 currentTarget;
    protected bool movingForward = true;

    void OnDrawGizmosSelected()
    {
        Vector2 endPointLeftDraw = new(endPointLeft.x, endPointLeft.y);
        Vector2 endPointRightDra = new(endPointRight.x, endPointRight.y);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(endPointLeftDraw, 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPointRightDra, 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(endPointLeftDraw, endPointRightDra);
    }

    void Start()
    {
        currentPoint = transform.position;
        currentTarget = endPointRight;
    }

    protected override void UpdateInput()
    {
        if (Vector2.Distance(endPointLeft, endPointRight) <= 0) return;

        if (Vector2.Distance(transform.position, currentTarget) <= 0)
        {
            if (movingForward)
            {
                currentTarget = endPointLeft;
                movingForward = false;
            }
            else
            {
                currentTarget = endPointRight;
                movingForward = true;
            }
        }

        MoveInput = currentTarget;
    }
}