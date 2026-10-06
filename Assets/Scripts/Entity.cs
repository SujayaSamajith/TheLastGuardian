using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = .1f;
    private Coroutine damageFeedbackCoroutine;


    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask WhatIsTargert;

    
    
    
    

    [Header("collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] protected bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;


    protected int facingDir = 1;
    protected bool facingRight = true;
    protected bool canMove = true;

    protected virtual  void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
       

        currentHealth = maxHealth;
    }
    protected virtual void Update()
    {

        handleCollision();

        
        HndleMovement();
        HandleAnimations();
        handleFlip();




    }

    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsTargert);

        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        playDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    private void playDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
        {
            StopCoroutine(damageFeedbackCoroutine);
        }
        StartCoroutine(damageFeedbackCo());
    }

    protected virtual void Die()
    {


        anim.enabled = false;
        col.enabled = false;
        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 3);

    }

    private IEnumerator damageFeedbackCo()
    {
        Material originalMat = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damageFeedbackDuration);
        sr.material = originalMat;
    }

    

    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
        
    }

    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    

    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
            
        }
    }

    protected  virtual void handleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            flip();


    }
    

    protected virtual void handleCollision()
    {
        isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            whatIsGround
        );

    }
    protected virtual void HndleMovement()
    {
        
    }

    public void flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    private void OnDrawGizmos()
    {
     
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-groundCheckDistance));

        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
  
    

