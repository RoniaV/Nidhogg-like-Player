using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection 
{
    private IInput PI;
    private Transform player;

    public ChangeDirection(IInput pi, Transform player)
    {
        PI = pi;
        this.player = player;
    }

    public void CheckDirection()
    {
        player.rotation = Quaternion.Euler(0, (PI.horizontal == -1 ? 180 : 0), 0);
    }
}
