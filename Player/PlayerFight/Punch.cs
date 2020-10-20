using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : IHit
{
    public bool hitting { get; private set; }

    private Transform player;
    private AnimationManager AM;

    private int hitLayer;
    private int playerLayer;
    private Vector2 realPunchStep;
    private int hitFrames;

    private int actualFrames;

    public Punch(Transform player, PlayerFightSettings PFS, AnimationManager am)
    {
        this.player = player;
        AM = am;
        hitLayer = PFS.HitLayer;
        playerLayer = PFS.PlayerLayer;
        realPunchStep = new Vector2(PFS.PunchStep, 0);
    }

    public void Hit()
    {
        if (!hitting)
        {
            hitting = true;
            player.gameObject.layer = hitLayer;
            player.position += (Vector3)realPunchStep;
            AM.SetTrigger("Punch", false);
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
        actualFrames = 0;
    }
}
