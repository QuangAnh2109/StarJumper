using UnityEngine;

public class GroundMovement : EntityMovement
{
    [SerializeField] protected Vector2 endPointLeft;
    [SerializeField] protected Vector2 endPointRight;
    [SerializeField] protected int loop = -1;

    protected Vector2 currentPoint;
    protected Vector2 currentTarget;
    protected bool movingForward = true;

    void OnDrawGizmosSelected()
    {
        Vector2 endPointLeftDraw = new(endPointLeft.x - 0.5f, endPointLeft.y + 0.5f);
        Vector2 endPointRightDra = new(endPointRight.x - 0.5f, endPointRight.y + 0.5f);

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

    protected override void Move()
    {
        if (Vector2.Distance(endPointLeft, endPointRight) <= 0) return;

        if (Vector2.Distance(transform.position, currentTarget) <= 0)
        {
            if(movingForward)
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

        Vector2 newPosition = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.fixedDeltaTime);
        Rigidbody.MovePosition(newPosition);
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