using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Player : Character
{
    public KeySet keys;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected internal Vector2 movement = Vector2.zero;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This is protected and NOT virtual so that it cannot be overriden by child classes.
    /// </summary>
    protected void Update()
    {
        movement = Vector2.zero;
        if (Input.GetKey(keys.LeftKey)) movement.x--;
        if (Input.GetKey(keys.RightKey)) movement.x++;
        if (Input.GetKey(keys.UpKey)) movement.y++;
        if (Input.GetKey(keys.DownKey)) movement.y--;

        if (movement != Vector2.zero)
        {
            //standardize the movement vector so that its magnitude is 1.
            movement.Normalize();
        }

        if (Input.GetKey(keys.ActionKey))
        {
            PerformAction();
        }

        PostUpdate();
    }

    private void FixedUpdate()
    {
        body.AddForce(movement * speed, ForceMode2D.Impulse);
        if (body.velocity.x <= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        animator.SetFloat("Speed", body.velocity.magnitude);
    }


    public abstract void PerformAction();
    public abstract void PostUpdate();
}
