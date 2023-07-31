using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintainer : Player
{
    public float maintainRadius = 1.5f;
    public float maintainOffsetY = 0;
    public int repairAmount = 5;

    public float repairInterval = 0.25f;

    private float repairTimer = 0f;

    public override void PerformAction()
    {
        repairTimer -= Time.deltaTime;

        if (repairTimer <= 0)
        {
            repairTimer = repairInterval;
            Collider2D[] colls = Physics2D.OverlapCircleAll((Vector2)transform.position + Vector2.down * maintainOffsetY, maintainRadius);
            foreach (Collider2D col in colls)
            {
                if (col.GetComponent<Building>() is Building building && building is not Spaceship)
                {
                    building.Repair(repairAmount);
                }
            }
        }
    }

    public override void PostUpdate()
    {
        if (FindObjectOfType<Builder>().movement.magnitude > 0.1f)
        {
            movement *= 0.5f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.down * maintainOffsetY, maintainRadius);
    }
}
