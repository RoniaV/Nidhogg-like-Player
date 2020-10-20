using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : IHit
{
    public bool hitting { get; private set; }
    
    private int hitFrames;

    private int actualFrames;
    private Weapon actualWeapon;

    public ThrowWeapon(PlayerFightSettings PFS)
    {
        hitFrames = PFS.ThrowWeaponFrames;
    }

    public void Hit()
    {
        if (!hitting)
        {
            hitting = true;
            actualWeapon.Throw();
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
        actualFrames = 0;
    }

    public void SwitchWeapon(Weapon weapon)
    {
        actualWeapon = weapon;
    }
}
