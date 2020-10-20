using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight
{
    public bool isHitting { get; private set; }
    public bool hasWeapon { get; private set; }
    
    private AnimationManager AM;
    private AirKick AK;
    private DownKick DK;
    private Punch P;
    private WeaponAttack WA;
    private ThrowWeapon TW;

    private Weapon actualWeapon;
    private IHit actualHit;

    public PlayerFight(Transform player, BoxCollider2D collider, Rigidbody2D rb, Transform weaponPoint, PlayerFightSettings PFS, Weapon weapon, AnimationManager am)
    {
        AM = am;
        AK = new AirKick(player, collider, rb, ColliderOffset(PFS.AirKickSize), PFS, AM);
        DK = new DownKick(player, collider, ColliderOffset(PFS.DownKickSize), PFS, AM);
        P = new Punch(player, PFS, AM);
        WA = new WeaponAttack(weaponPoint, weapon, AM);
        TW = new ThrowWeapon(PFS);
    }

    public void SelectHit(bool isJumping, bool isGrounded, bool isCrouched, bool holdingUp)
    {
        if(holdingUp && hasWeapon)
        {
            isHitting = true;
            TW.Hit();
            actualHit = TW;
        }
        else if (isJumping)
        {
            isHitting = true;
            AK.Hit();
            actualHit = AK;
        }
        else if (isGrounded && isCrouched)
        {
            isHitting = true;
            DK.Hit();
            actualHit = DK;
        }
        else if (hasWeapon && !isJumping)
        {
            isHitting = true;
            WA.Hit();
            actualHit = WA;
        }
        else if (isGrounded)
        {
            isHitting = true;
            P.Hit();
            actualHit = P;
        }
        Debug.Log(actualHit + " started");
    }

    public void FinishHit()
    {
        actualHit.Hit();
        isHitting = actualHit.hitting;
        actualHit = isHitting ? actualHit : null;
        if (actualHit == WA)
            actualWeapon.FollowBone(false);
        Debug.Log(actualHit);
    }

    public void WeaponSwitch(Weapon w)
    {
        actualWeapon = w;
        hasWeapon = actualWeapon != null;
        TW.SwitchWeapon(w);

        AM.SetBool("hasWeapon", hasWeapon);
    }

    public void StopHit()
    {
        if (actualHit != null)
            actualHit.ExitHit();

        isHitting = false;
        actualHit = null;
    }
    
    private Vector2 ColliderOffset(Vector2 colliderSize)
    {
        Vector2 colliderOffset = new Vector2();
        colliderOffset.y = colliderSize.y / 2;
        return colliderOffset;
    }

}
