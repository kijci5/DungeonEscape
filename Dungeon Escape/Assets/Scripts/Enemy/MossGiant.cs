using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy,IDamageable {
    public int Health { get; set; }
    public void Damage(int damageAmount)
    {
        throw new System.NotImplementedException();
    }
}
