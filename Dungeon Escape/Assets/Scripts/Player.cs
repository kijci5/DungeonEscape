﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed=3f;
    [SerializeField]
    private float jumpForce = 5f;

    private Rigidbody2D rigidBody;
    private bool isGrounded=false;
    private bool resetJumpNeeded = false;

    private PlayerAnimation pAnimation;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	    pAnimation = GetComponent<PlayerAnimation>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Movement();

	    CheckGrounded();
	}

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if (hit.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                isGrounded = true;
            }
        }
    }

    private void Movement()
    {
        float move = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            isGrounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
        }

        rigidBody.velocity = new Vector2(move*speed, rigidBody.velocity.y);
        pAnimation.Move(move);
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}