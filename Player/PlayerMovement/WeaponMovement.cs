using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement 
{
    public bool holdingUp { get; private set; }

    private float weaponMov;

    private Transform weaponPoint;
    private IInput PI;
    private AnimationManager AM;

    private Weapon actualWeapon;
    private int actualWeaponPos;
    private Vector2 realWeaponMov;
    private bool wMoved;

    public WeaponMovement(Transform weaponPoint, IInput pi, PlayerFightSettings PFS, AnimationManager am)
    {
        this.weaponPoint = weaponPoint;
        PI = pi;
        AM = am;
        weaponMov = PFS.WeaponMov;

        actualWeaponPos = 1;
        realWeaponMov = new Vector2(0, weaponMov);
    }

    public void MovePoint()
    {
        if (actualWeapon != null)
        {
            if (PI.vertical != 0 && !wMoved && CheckPos())
            {
                wMoved = true;
                //weaponPoint.position += (Vector3)realWeaponMov * Mathf.Sign(PI.vertical);
                actualWeaponPos += (int)Mathf.Sign(PI.vertical);
                AM.SetTrigger("WMove", false);
                AM.SetInt("WPos", actualWeaponPos);
            }
            else if (Mathf.Abs(PI.vertical) == 1)
            {
                holdingUp = true;
            }
        }

        if (PI.vertical == 0 || actualWeapon == null)
        {
            holdingUp = false;
            wMoved = false;
        }
        AM.SetBool("holdingUp", holdingUp);
    }

    public void SwitchWeapon(Weapon newWeapon)
    {
        actualWeapon = newWeapon;
    }
    
    private bool CheckPos()
    {
        return actualWeaponPos + Mathf.Sign(PI.vertical) <= actualWeapon.w.MaxPos && 
            actualWeaponPos + Mathf.Sign(PI.vertical) >= actualWeapon.w.MinPos;
    }
}
