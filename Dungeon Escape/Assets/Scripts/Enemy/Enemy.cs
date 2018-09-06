using System.Collections;
using System.Collections.Generic;
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

    protected virtual void Attack()
    {

    }

    protected virtual void Move()
    {
        if (transform.position.x <= pointA.position.x)
        {
            currentTarget = pointB.position;
        }
        else if (transform.position.x >= pointB.position.x)
        {
            currentTarget = pointA.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    public abstract void Update();
}
