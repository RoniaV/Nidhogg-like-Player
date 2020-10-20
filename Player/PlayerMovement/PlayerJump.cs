using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    public bool isJumping { get; private set; }

    private float jumpForce;
    private LayerMask groundLayer;

    private Rigidbody2D RB;
    private Grounded grounded;
    private IInput PI;
    private AnimationManager AM;

    public PlayerJump(Rigidbody2D rb, IInput input, BoxCollider2D collider, Grounded grounded, PlayerSettings PS, AnimationManager am)
    {
        RB = rb;
        PI = input;
        AM = am;
        this.grounded = grounded;

        PI.OnJump += Jump;

        jumpForce = PS.JumpForce;
        groundLayer = PS.GroundLayer;
    }
    
    private void Jump()
    {
        if(grounded.IsGrounded())
        {
            isJumping = true;
            RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AM.SetBool("isJumping", true);
        }
    }

    public void GroundCheck()
    {
        if (isJumping && RB.velocity.y < 0)
        {
            isJumping = !grounded.IsGrounded();
            AM.SetBool("isJumping", isJumping);
        }
    }
}
