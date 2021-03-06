﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,IDamageable
{
    public GameObject diamondPrefab;
    public int Health { get; set; }

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int numberOfDiamonds;
    [SerializeField]
    protected float areaOfAttack;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Animator animator;
    protected bool isHit=false;
    protected bool isDead = false;

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

        if (isDead == false)
        {
            Movement();
        }
    }

    protected virtual void Movement()
    {
        FlipWhileMoving();

        MovingBetweenPoints();

        if (isHit == false && !animator.GetBool("InCombat"))
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        if (DistanceBetweenPlayer() > areaOfAttack || player.Health<=0)
        {
            GetOutOfCombat();
        }
        else if(DistanceBetweenPlayer()<areaOfAttack)
        { animator.SetBool("InCombat",true);}

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


    public virtual void Damage(int damageAmount)
    {
        if (isDead)
        { return; }
        animator.SetTrigger("Hit");
        isHit = true;
        Health -= damageAmount;
        animator.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            animator.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().diamondValue = numberOfDiamonds;
        }
    }
}
