using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : IHit
{
    public bool hitting { get; private set; }

    private float weaponStep;
    private int hitFrames;

    private Transform weaponPoint;
    private AnimationManager AM;

    private WeaponSO actualWeapon;

    private int actualFrames;
    private Vector2 realWeaponStep;
    private Vector3 weaponPointPos;

    public WeaponAttack(Transform weaponPoint, Weapon weapon, AnimationManager am)
    {
        this.weaponPoint = weaponPoint;
        AM = am;
        if (weapon != null)
        {
            actualWeapon = weapon.w;
            weaponStep = actualWeapon.Step;
            hitFrames = actualWeapon.WeaponFrames;
        }

        realWeaponStep = new Vector2(weaponStep, 0);
    }

    public void Hit()
    {
        if (!hitting)
        {
            hitting = true;
            AM.SetTrigger("WAttack", false);
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

    public void SwitchWeapon(WeaponSO newWeapon)
    {
        actualWeapon = newWeapon;
    }
}
