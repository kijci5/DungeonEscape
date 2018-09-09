using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageable
{

    public GameObject acidEffectPrefab;

    public override void Damage(int damageAmount)
    {
        if (isDead)
        { return; }
        Health -= damageAmount;
        animator.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, this.transform.position, Quaternion.identity);
    }
}
