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

    public override void UpdateInput()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GroundMovement: OnCollisionEnter2D called with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GroundMovement: Player collided, setting parent to " + transform.name);
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("GroundMovement: OnCollisionExit2D called with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GroundMovement: Player exited collision, removing parent from " + collision.gameObject.name);
            collision.transform.parent = null;
        }
    }
}