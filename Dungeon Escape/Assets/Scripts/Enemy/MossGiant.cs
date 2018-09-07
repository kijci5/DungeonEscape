using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy {

    public override void Update ()
    {
        if (IsIdleState())
        {
            return;
        }
	    base.Move();
	}
}
