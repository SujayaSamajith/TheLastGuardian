using UnityEngine;

public class ObjectToProtect : Entity
{
    [SerializeField] private Transform player;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        handleCollision();
        HandleAnimations();
        handleFlip();
    }

    protected override void handleFlip()
    {
        if (player == null)
            return;

        if (player.position.x > transform.position.x && !facingRight)
        {
            flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            flip();
        }
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }

}