using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public int DiamondsCollected = 0;

    [SerializeField]
    private float speed=3f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private int health = 4;

    private Rigidbody2D rigidBody;
    private PlayerAnimation pAnimation;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer swordSpriteRenderer;
    private Animator animator;
    private bool isGrounded = false;
    private bool resetJumpNeeded = false;
    private bool isDead = false;

    public int Health { get; set; }

    private void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	    pAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        swordSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        Health = health;
    }
	
	// Update is called once per frame
    private void Update ()
    {
        if (isDead)
        { return;}
        
	    Move();

	    CheckGrounded();

	    if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded)
	    {
            pAnimation.Attack();
	    }
	}

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if (hit.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                isGrounded = true;
                pAnimation.Jump(isGrounded);
            }
        }
    }

    private void Move()
    {
        float move = Input.GetAxis("Horizontal");

        Flip(move);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            isGrounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
            pAnimation.Jump(isGrounded);
        }

        rigidBody.velocity = new Vector2(move*speed, rigidBody.velocity.y);
        pAnimation.Move(move);
    }

    private void Flip(float move)
    {
        if (move > 0)
        {
            spriteRenderer.flipX = false;
            swordSpriteRenderer.flipX = false;
            swordSpriteRenderer.flipY = false;

            Vector3 newPos = swordSpriteRenderer.transform.localPosition;
            newPos.x = 1.01f;
            swordSpriteRenderer.transform.localPosition = newPos;
        }
        else if (move < 0)
        {
            spriteRenderer.flipX = true;
            swordSpriteRenderer.flipX = true;
            swordSpriteRenderer.flipY = true;

            Vector3 newPos = swordSpriteRenderer.transform.localPosition;
            newPos.x = -1.01f;
            swordSpriteRenderer.transform.localPosition = newPos;
        }
    }

    private IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;

        UIManager.Instance.UpdateLives(Health);

        if (Health < 1 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    public void AddGems(int amount)
    {
        DiamondsCollected += amount;
        UIManager.Instance.UpdateGemCount(DiamondsCollected);
    }
}
