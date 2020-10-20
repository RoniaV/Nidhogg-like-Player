using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownKick : IHit
{
    public bool hitting { get; private set; }

    private Transform player;
    private BoxCollider2D collider;
    private AnimationManager AM;

    private int kickLayer;
    private int playerLayer;
    private Vector2 downKickSize;
    private Vector2 downKickOffset;
    private int hitFrames;

    private int actualFrames;
    private Vector2 realSize, realOffset;

    public DownKick(Transform player, BoxCollider2D collider, Vector2 offset, PlayerFightSettings PFS, AnimationManager am)
    {
        this.player = player;
        this.collider = collider;
        AM = am;
        kickLayer = PFS.HitLayer;
        playerLayer = PFS.PlayerLayer;
        downKickSize = PFS.DownKickSize;
        downKickOffset = offset;
        hitFrames = PFS.DownKickFrames;

        realSize = this.collider.size;
        realOffset = this.collider.offset;
    }

    public void Hit()
    {
        if (!hitting)
        {
            hitting = true;
            player.gameObject.layer = kickLayer;
            collider.size = downKickSize;
            collider.offset = downKickOffset;

            AM.SetTrigger("DownKick", false);
        }
        else
        {
            actualFrames++;
            if (actualFrames >= hitFrames)
                ExitHit();
        }
    }

    public void ExitHit()
    {
        hitting = false;
        player.gameObject.layer = playerLayer;
        collider.size = realSize;
        collider.offset = realOffset;
        actualFrames = 0;
    }

}
