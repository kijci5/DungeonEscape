using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator playerAnimator;
    private Animator swordAnimator;

    private void Start ()
	{
	    playerAnimator = GetComponentInChildren<Animator>();
	    swordAnimator = transform.GetChild(1).GetComponent<Animator>();
	}

    public void Move(float move)
    {
       playerAnimator.SetFloat("Move",Mathf.Abs(move));
    }

    public void Jump(bool isGrounded)
    {
        if(!isGrounded)
        {
            playerAnimator.SetBool("Jumping", true);
        }
        else if (isGrounded)
        {
            playerAnimator.SetBool("Jumping", false);
        }
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        swordAnimator.SetTrigger("SwordAnimation");
    }
}
