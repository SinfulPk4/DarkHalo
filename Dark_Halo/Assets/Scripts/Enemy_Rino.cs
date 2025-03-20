using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino : Enemy
{
    [Header("Rino Details")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedUpRate = 0.6f;
    [SerializeField] private Vector2 impactPower;
    [SerializeField] private float detectionRange;
    private bool playerDetected;
    private float defaultSpeed;

    protected override void Start()
    {
        base.Start();

        defaultSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("xVelocity", rbEnemy.velocity.x);

        HandleCollision();
        HandleCharge();
    }

    private void HandleCharge()
    {
        if (canMove == false)
        {
            return;
        }

        moveSpeed = moveSpeed + (Time.deltaTime * speedUpRate);

        if(moveSpeed >= maxSpeed)
        {
            maxSpeed = moveSpeed;
        }

        rbEnemy.velocity = new Vector2(moveSpeed * facingDir, rbEnemy.velocity.y);

        if (isWallDetected)
        {
            WallHit();
        }

        if (!isGroundInfrontDetected)
        {
            TurnAround();
        }
    }

    private void TurnAround()
    {
        moveSpeed = defaultSpeed;
        canMove = false;
        rbEnemy.velocity = Vector2.zero;
        Flip();
    }

    private void WallHit()
    {
        canMove = false;
        moveSpeed = defaultSpeed;
        anim.SetBool("hitWall", true);
        rbEnemy.velocity = new Vector2(impactPower.x * -facingDir, impactPower.y);
    }

    private void ChargeIsOver()
    {
        anim.SetBool("hitWall", false);
        Invoke(nameof(Flip), 1);
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);

        if (playerDetected && isGrounded)
        {
            canMove = true;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (detectionRange * facingDir), transform.position.y));
    }
}
