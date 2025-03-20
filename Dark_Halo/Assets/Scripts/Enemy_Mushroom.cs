using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    protected override void Update()
    {
        base.Update();

        anim.SetFloat("xVelocity", rbEnemy.velocity.x);

        if(isDead)
        {
            return;
        }

        HandleCollision();
        HandleMovement();

        if(isGrounded)
        {
            HandleTurnAround();
        }
    }

    private void HandleTurnAround()
    {
        if (!isGroundInfrontDetected || isWallDetected)
        {
            Flip();
            idleTimer = idleDuration;
            rbEnemy.velocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if(idleTimer > 0)
        {
            return;
        }

        rbEnemy.velocity = new Vector2(moveSpeed * facingDir, rbEnemy.velocity.y);
    }
}
