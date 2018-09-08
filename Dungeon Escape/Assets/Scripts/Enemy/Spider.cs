using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageable {
    public override void Damage(int damageAmount)
    {
        Health -= damageAmount;
        animator.SetBool("InCombat", true);

        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }
}
