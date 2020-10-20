using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch
{
    public bool isCrouched { get; private set; }

    private Vector2 crouchSize;
    private float crouchVelocity;
    private float rollLenght;

    private Vector2 realSize, colliderOffset;
    private bool rolled;
    private float rollT;

    private IInput PI;
    private AnimationManager AM;
    private PlayerMovement PM;
    private BoxCollider2D boxCollider;

    public PlayerCrouch(IInput input, PlayerMovement pm, BoxCollider2D collider, PlayerSettings PS, AnimationManager am)
    {
        PI = input;
        AM = am;
        PM = pm;
        boxCollider = collider;

        crouchSize = PS.CrouchSize;
        crouchVelocity = PS.CrouchVelocity;
        rollLenght = PS.RollLenght;

        realSize = boxCollider.size;
        colliderOffset = new Vector2(0, realSize.y / 2);
    }
    
    public void Crouch()
    {
        if(PI.vertical == -1)
        {
            if (!isCrouched)
            {
                boxCollider.size = crouchSize;
                boxCollider.offset = ColliderOffset(crouchSize);
            }

            isCrouched = true;
            
            if (!CheckRoll())
            {
                rolled = true;
                PM.SetVelocity(crouchVelocity);
            }
        }
        else if(PI.vertical >= 0 && isCrouched)
        {
            isCrouched = false;
            rolled = false;

            boxCollider.size = realSize;
            boxCollider.offset = ColliderOffset(realSize);
            
            PM.SetVelocity();
        }

        AM.SetBool("isCrouched", isCrouched);
    }

    private bool CheckRoll()
    {
        if (PM.isRunning && !rolled)
        {
            rollT += Time.deltaTime;

            if (rollT >= rollLenght)
            {
                rolled = true;
                rollT = 0;
                return false;
            }

            return true;
        }
        else
            return false;
    }

    private Vector2 ColliderOffset(Vector2 colliderSize)
    {
        colliderOffset.y = colliderSize.y / 2;
        return colliderOffset;
    }
}
