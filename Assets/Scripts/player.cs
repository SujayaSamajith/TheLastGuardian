using Unity.VisualScripting;
using UnityEngine;


public class player : Entity
{
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool canJump = true;
    

    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;


    protected override void Update()
    {
        base.Update();
        HandleInput();
    }
    protected override void HndleMovement()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void TryToJump()
    {


        
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();


    }

    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        canJump = enable;
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }
}
