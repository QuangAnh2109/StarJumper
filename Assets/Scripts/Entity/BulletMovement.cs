using UnityEngine;

public class BulletMovement : EntityMovement
{
    [SerializeField] private float lifeTime = 5f;

    private float timeEnd;

    private void Start()
    {
        timeEnd = Time.time + lifeTime;
    }
    protected override void Move()
    {
        if(Time.time > timeEnd)
        {
            Destroy(gameObject);
        }
        else
        {
            base.Rigidbody.AddForce(transform.forward * moveSpeed, ForceMode2D.Impulse);
        }
    }
}