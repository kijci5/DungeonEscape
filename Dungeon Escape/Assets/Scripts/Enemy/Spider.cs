using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {

	public override void Update () {
	    if (IsIdleState())
	    {
	        return;
	    }
	    base.Move();
    }
}
