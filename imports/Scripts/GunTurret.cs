using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GunTurret : RangedBuilding
{
    public Transform rotatingHead;

    public override void Fire(Vector2 direction)
    {
        //(1, 0)   0
        //(0, -1)  pi/2

        float theta;
        if (direction.x <= 0)
        {
            theta = Mathf.Atan(direction.y / direction.x) + Mathf.PI;
        }
        else
        {
            theta = Mathf.Atan(direction.y / direction.x);
        }

        if (rotatingHead != null)
        {
            rotatingHead.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * theta));
        }

        Projectile p = Instantiate(projectile.gameObject, transform.position, rotatingHead == null ? Quaternion.identity : Quaternion.Euler(0, 0, rotatingHead.rotation.eulerAngles.z + 90), FindObjectOfType<ProjectilesHolder>().transform).GetComponent<Projectile>();
        p.forceDirection = direction;
    }
}
