using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Animator animator;
    protected bool isHit=false;

    private Vector3 currentTarget;
    private SpriteRenderer spriteRenderer;
    private Player player;

    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    public virtual void Update()
    {
        if (IsIdleState()&&animator.GetBool("InCombat")==false)
        {
            return;
        }
        Movement();
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Movement()
    {
        Flip();

        if (transform.position.x <= pointA.position.x)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position.x >= pointB.position.x)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }

        if (DistanceBetweenPlayer() > 2f)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }
    }

    private float DistanceBetweenPlayer()
    {
        return Vector3.Distance(transform.localPosition,player.transform.localPosition);
    }

    private bool IsIdleState()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    private void Flip()
    {
        spriteRenderer.flipX = currentTarget == pointA.position;
    }
}
