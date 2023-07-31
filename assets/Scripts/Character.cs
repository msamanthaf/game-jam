using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Character : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public Rigidbody2D body;

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
}
