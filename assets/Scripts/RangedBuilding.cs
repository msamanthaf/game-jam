using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBuilding : Building
{
    public float attackRadius;
    public Projectile projectile;
    public float firingRate;

    private float fireTimer = 0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    protected override void Update()
    {
        base.Update();
        
        if (curHP <= 0) return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            fireTimer = firingRate;

            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, attackRadius);

            Collider2D closest = null;
            float closestDist = float.PositiveInfinity;
            foreach (Collider2D coll in colls)
            {
                if (coll.GetComponent<Enemy>() is Enemy enemy)
                {
                    float dis = Vector2.Distance(transform.position, enemy.transform.position);//todo: optimize distance to sqr
                    if (dis < closestDist)
                    {
                        closestDist = dis;
                        closest = coll;
                    }
                }
            }

            if (closest != null)
            {
                Vector2 delta = closest.transform.position - transform.position;
                if (delta != Vector2.zero)
                {
                    delta.Normalize();
                }
                else
                {
                    delta = Vector2.up;
                }

                Fire(delta);
            }
        }
    }

    public virtual void Fire(Vector2 direction)
    {
        Projectile p = Instantiate(projectile.gameObject, transform.position, Quaternion.identity, FindObjectOfType<ProjectilesHolder>().transform).GetComponent<Projectile>();
        p.forceDirection = direction;
    }
}
