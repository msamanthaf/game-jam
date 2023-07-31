using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int HP;
    public int MaxHP;

    public float attackInterval;
    public int attackDamage;
    public float attackRadius;
    [HideInInspector]
    public float speedMultiplier = 1f;

    private float attackTimer;

    private void FixedUpdate()
    {
        Building targetBuilding = GetBuildingToAttack();

        if (targetBuilding != null)
        {
            body.velocity = Vector2.zero;
            // enemy is attacking a building.
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                targetBuilding.TakeDamage(attackDamage);
                attackTimer = attackInterval;
            }
        }
        else if (Spaceship.current != null)
        {
            //enemy is moving towards spaceship
            body.AddForce((Spaceship.current.transform.position - transform.position).normalized * speed * speedMultiplier, ForceMode2D.Impulse);
        }

        if (body.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private Building GetBuildingToAttack()
    {
        Building targetBuilding = null;
        var colls = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        Ditch ditch = null;
        foreach (var col in colls)
        {
            if (col.GetComponent<Building>() is Building building && building.curHP > 0)
            {
                if (building is Ditch d)
                {
                    ditch = d;
                }
                else
                {
                    targetBuilding = building;
                    break;
                }
            }
        }

        if (ditch != null)
        {
            speedMultiplier = ditch.slowDownMultiplier;
        }
        else
        {
            speedMultiplier = 1f;
        }

        return targetBuilding;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
