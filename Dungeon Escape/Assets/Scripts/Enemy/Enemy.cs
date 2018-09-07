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

    private Vector3 currentTarget;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public abstract void Update();

    protected virtual void Attack()
    {

    }

    protected virtual void Move()
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
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    protected bool IsIdleState()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    private void Flip()
    {
        spriteRenderer.flipX = currentTarget == pointA.position;
    }
}
