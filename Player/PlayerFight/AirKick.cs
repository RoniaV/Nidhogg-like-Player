using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirKick : IHit
{
    public bool hitting { get; private set; }

    private Transform player;
    private BoxCollider2D collider;
    private Rigidbody2D RB;
    private AnimationManager AM;

    private int hitLayer;
    private int playerLayer;
    private Vector2 airKickSize;
    private Vector2 airKickOffset;
    private int layersToKick;
    private float kickForce;

    private float realGravity;
    private Vector2 realSize, realOffset;
    
    public AirKick(Transform player, BoxCollider2D collider, Rigidbody2D rb, Vector2 offset, PlayerFightSettings PFS, AnimationManager am)
    {
        this.player = player;
        this.collider = collider;
        RB = rb;
        AM = am;
        hitLayer = PFS.HitLayer;
        playerLayer = PFS.PlayerLayer;
        airKickSize = PFS.AirKickSize;
        airKickOffset = offset;
        layersToKick = PFS.AirKickLayers;
        kickForce = PFS.AirKickForce; 

        realGravity = RB.gravityScale;
        realSize = this.collider.size;
        realOffset = this.collider.offset;
    }
    
    public void Hit()
    {
        if (!hitting)
        {
            hitting = true;
            player.gameObject.layer = hitLayer;
            collider.size = airKickSize;
            collider.offset = airKickOffset;
            RB.velocity = (Vector2.down + (Vector2)player.right) * kickForce;
            RB.gravityScale = 0;
            AM.SetTrigger("AirKick", false);
        }
        else
        {
            if (RB.IsTouchingLayers(layersToKick))
                ExitHit();
        }
    }

    public void ExitHit()
    {
        hitting = false;
        player.gameObject.layer = playerLayer;
        collider.size = realSize;
        collider.offset = realOffset;
        RB.velocity = Vector2.zero;
        RB.gravityScale = realGravity;

    }
}
