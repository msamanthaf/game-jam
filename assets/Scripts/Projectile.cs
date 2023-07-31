using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private const float SELF_EXPLODE_RADIUS = 1f;
    private const float SELF_EXPLODE_TIME = 10f;

    public int damage;
    [Header("-1 radius means no AOE")]
    public float aoeRadius;
    public float speed;
    public bool overWalls = false;

    private float selfExplodeTimer = 10f;

    [HideInInspector]
    public Vector2 forceDirection;
    [HideInInspector]
    public Vector2 targetPosition;
    [HideInInspector]
    public Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = forceDirection * speed;
        selfExplodeTimer = SELF_EXPLODE_TIME;
    }

    private void Update()
    {
        selfExplodeTimer -= Time.deltaTime;
        if (selfExplodeTimer <= 0 || Vector2.Distance(targetPosition, transform.position) <= SELF_EXPLODE_RADIUS)
        {
            if (aoeRadius != -1)
            {
                //area damage
                Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
                foreach (Collider2D collider in allColliders)
                {
                    //single target damage
                    if (collider.GetComponent<Enemy>() is Enemy enemy)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aoeRadius == -1)
        {
            //single target damage
            if (collision.GetComponent<Enemy>() is Enemy enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
        else
        {
            //area damage
            Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
            foreach (Collider2D collider in allColliders)
            {
                //single target damage
                if (collider.GetComponent<Enemy>() is Enemy enemy)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }
}
