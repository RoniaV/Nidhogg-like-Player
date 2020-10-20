using System;
using UnityEngine;

public class PickWeapon
{
    private IInput PI;
    private CollisionDetection CD;
    private Transform weaponPoint, bonePoint;

    public event CollisionDetection.WeaponDelegate OnPick;

    public PickWeapon(IInput pi, CollisionDetection cd, Transform weaponPoint, Transform bonePoint)
    {
        PI = pi;
        CD = cd;
        this.weaponPoint = weaponPoint;
        this.bonePoint = bonePoint;

        CD.PickableCollision += Pick;
    }

    public void Pick(Weapon weapon)
    {
        if(PI.vertical < 0)
        {
            weapon.Picked(weaponPoint, bonePoint);
            OnPick(weapon);
        }
    }
}
