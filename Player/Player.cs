using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CollisionDetection))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool dead { get; private set; }

    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private PlayerFightSettings playerFightSettings;
    [SerializeField]
    private Weapon initialWeapon;
    [SerializeField]
    private Transform weaponPoint, bonePoint;

    private IInput PI;
    private CollisionDetection CD;
    private AnimationManager AM;
    private Grounded grounded;
    private ChangeDirection ChD;
    private PlayerMovement PM;
    private PlayerJump PJ;
    private PlayerCrouch PC;
    private WeaponMovement WM;
    private PickWeapon PW;
    private Kicked K;

    private PlayerFight PF;

    private delegate void WeaponDelegate(Weapon w);
    private event WeaponDelegate OnWeaponSwitch;

    private Weapon actualWeapon;

    void Awake()
    {
        #region Instantiate classes
        switch (playerSettings.InputController)
        {
            case 1:
                PI = new PlayerInput(playerSettings.InputSet);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        CD = GetComponent<CollisionDetection>();
        AM = new AnimationManager(GetComponent<Animator>());
        grounded = new Grounded(GetComponent<BoxCollider2D>(), playerSettings.GroundLayer);
        ChD = new ChangeDirection(PI, transform);
        PM = new PlayerMovement(transform, PI, playerSettings, AM);
        PJ = new PlayerJump(GetComponent<Rigidbody2D>(), PI, GetComponent<BoxCollider2D>(), grounded, playerSettings, AM);
        PC = new PlayerCrouch(PI, PM, GetComponent<BoxCollider2D>(), playerSettings, AM);
        WM = new WeaponMovement(weaponPoint, PI, playerFightSettings, AM);
        PW = new PickWeapon(PI, CD, weaponPoint, bonePoint);
        K = new Kicked(PI, CD, playerSettings, AM);

        PF = new PlayerFight(transform, GetComponent<BoxCollider2D>(), GetComponent<Rigidbody2D>(), weaponPoint, playerFightSettings, initialWeapon, AM);
        #endregion

        #region Subscribe classes to events
        PI.OnAttack += Attack;
        CD.WeaponCollided += Dead;
        CD.KickCollided += CheckDead;
        PW.OnPick += SetWeapon;

        OnWeaponSwitch += WM.SwitchWeapon;
        OnWeaponSwitch += PF.WeaponSwitch;
        #endregion        

        if (initialWeapon != null)
            SetWeapon(Instantiate(initialWeapon));
    }

    private void FixedUpdate()
    {
        if (!K.isKicked && !dead)
        {
            //Movement is set in FixedUpdate to avoid frame rate change
            PM.Move(!PF.isHitting);
            PM.CanStepWalk(!PJ.isJumping && !PC.isCrouched);
        }
    }

    private void Update()
    {
        if (!dead)
        {
            //First read Input
            PI.ReadInput();

            //and then check conditions for class calls
            if (!K.isKicked)
            {
                PJ.GroundCheck();
                ChD.CheckDirection();

                if (actualWeapon != null)
                {
                    if (!PM.isRunning && !PJ.isJumping && !PC.isCrouched && !WM.holdingUp)
                        actualWeapon.FollowBone(false);
                    else
                        actualWeapon.FollowBone(true);
                }

                if (!PF.isHitting)
                {
                    PC.Crouch();
                }

                if (!PJ.isJumping && !PC.isCrouched && !PF.isHitting)
                {
                    WM.MovePoint();
                }

                if (PF.isHitting)
                {
                    PF.FinishHit();
                }

            }
            else
                K.GetUp();
        }
    }

    private void Attack()
    {
        if(!PF.isHitting)
        PF.SelectHit(PJ.isJumping, grounded.IsGrounded(), PC.isCrouched, WM.holdingUp);
    }

    private void SetWeapon(Weapon weapon = null)
    {
        if (weapon != null)
        {
            weapon.Picked(weaponPoint, bonePoint);
            weapon.OnThrow += SetWeapon;
        }

        actualWeapon = weapon;
        OnWeaponSwitch(weapon);
    }

    private void SetWeapon()
    {
        SetWeapon(null);
    }

    private void CheckDead()
    {
        Debug.Log("check dead");
        //if (K.isKicked)
            //Dead(null);
    }

    private void Dead(Weapon weapon)
    {
        if (weapon != actualWeapon || weapon == null)
        {
            Debug.Log("dead");
            dead = true;
            PF.StopHit();
            AM.SetTrigger("Dead");
        }
    }
    
    private void Debuging()
    {
        Debug.Log(PJ.isJumping + " " + grounded.IsGrounded() + " " + PC.isCrouched);
    }
}
