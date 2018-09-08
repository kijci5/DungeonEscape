using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IDamageable
{
    public int Health { get; set; }

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
        Health = health;
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
        FlipWhileMoving();

        MovingBetweenPoints();

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        if (DistanceBetweenPlayer() > 2f)
        {
            GetOutOfCombat();
        }

        FacePlayerInCombat();
    }

    private void MovingBetweenPoints()
    {
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
    }

    private void GetOutOfCombat()
    {
        isHit = false;
        animator.SetBool("InCombat", false);
    }

    private void FacePlayerInCombat()
    {
        Vector3 distance = player.transform.localPosition - transform.localPosition;
        if (distance.x > 0f && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = false;
        }
        else if (distance.x < 0f && animator.GetBool("InCombat"))
        {
            spriteRenderer.flipX = true;
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

    private void FlipWhileMoving()
    {
        spriteRenderer.flipX = currentTarget == pointA.position;
    }


    public void Damage(int damageAmount)
    {
        animator.SetTrigger("Hit");
        isHit = true;
        Health -= damageAmount;
        animator.SetBool("InCombat", true);

        if (Health < 1)
        {
            Destroy(gameObject);
        }
    }
}
