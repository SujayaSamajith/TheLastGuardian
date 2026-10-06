using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : Entity
{

    private EnemyRespawner respawner;
    private bool playerDetected;

    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    private bool isDead;



    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }


    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("attack");
        }
    }



    protected override void HndleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    protected override void handleCollision()
    {
        base.handleCollision();

        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, WhatIsTargert);
    }

    protected override void Die()
    {
        if (isDead)
            return;

        isDead = true;

        // Count the kill first
        if (UI.instance != null)
            UI.instance.AddKillCount();

        // Tell respawner this enemy died
        if (respawner != null)
            respawner.EnemyDied();

        // Play death animation/falling
        base.Die();
    }
    public void SetRespawner(EnemyRespawner respawner)
    {
        this.respawner = respawner;
    }

}
