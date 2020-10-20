using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded
{
    private BoxCollider2D collider;

    private LayerMask groundLayer;
    private Vector2 boxCastSize;

    public Grounded( BoxCollider2D collider, LayerMask groundLayer)
    {
        this.collider = collider;
        this.groundLayer = groundLayer;
        boxCastSize = new Vector2(collider.bounds.size.x, 0.02f);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.transform.position, boxCastSize, 0, Vector2.down, 0, groundLayer);
    }
}
