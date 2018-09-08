using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy,IDamageable {
    public int Health { get; set; }

    public override void Start()
    {
        base.Start();
        Health = base.health;
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("Taking damage");
        animator.SetTrigger("Hit");
        isHit = true;
        Health -= damageAmount;
        animator.SetBool("InCombat",true);

        if (Health < damageAmount)
        {
            Destroy(gameObject);
        }
    }
}
