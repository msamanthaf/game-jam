using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    [Header("Particles instantiated when building got hit.")]
    public ParticleSystem hitParticles;
    [Header("Particles instantiated when building is destroyed.")]
    public ParticleSystem dustParticles;
    [Header("Particles instantiated when building is being repaired.")]
    public ParticleSystem repairParticles;

    public int maxHP = 5000;
    public int curHP = 5000;

    [Header("How much damage is taken per second?")]
    public int deteriationRate = 1;

    private float deteriationTimer = 1f;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        // deteriate this building over time!
        deteriationTimer -= Time.deltaTime;
        if (deteriationTimer <= 0)
        {
            TakeDamageNoParticles(deteriationRate);
            deteriationTimer = 1;
        }

        if (curHP <= 0)
        {
            curHP = 0;
            spriteRenderer.color = new Color(0, 0, 0, 0.7f);
        }
        else
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, (float)curHP / maxHP);
        }
    }

    public void TakeDamageNoParticles(int damage)
    {
        if (damage > 0)
        {
            curHP -= damage;
            if (curHP <= 0)
            {
                curHP = 0;
                OnBuildingIsDestroyed();
            }
        }
    }

    public void Repair(int amount)
    {
        if (repairParticles != null)
            Instantiate(repairParticles, transform.position, Quaternion.identity, FindObjectOfType<ParticlesHolder>().transform);

        curHP += amount;
        curHP = Mathf.Min(curHP, maxHP);
    }

    public void TakeDamage(int damage)
    {
        TakeDamageNoParticles(damage);
        if (hitParticles != null)
            Instantiate(hitParticles, transform.position, Quaternion.identity, FindObjectOfType<ParticlesHolder>().transform);
    }

    public virtual void OnBuildingIsDestroyed()
    {
        if (dustParticles != null)
            Instantiate(dustParticles, transform.position, Quaternion.identity, FindObjectOfType<ParticlesHolder>().transform);
    }
}
