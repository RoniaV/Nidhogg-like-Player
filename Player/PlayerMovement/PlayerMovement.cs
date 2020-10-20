using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    public bool isRunning { get; private set; }
    
    private float velocity;
    private float step;

    private bool canStep;
    private bool stepped;
    private Vector2 realStep;
    private float actualVel;

    private Transform player;
    private IInput PI;
    private AnimationManager AM;

    public PlayerMovement(Transform player, IInput input, PlayerSettings PS, AnimationManager am)
    {
        this.player = player;
        PI = input;
        AM = am;

        velocity = PS.Velocity;
        step = PS.Step;

        realStep = new Vector2(step, 0);
        actualVel = velocity;
        canStep = true;
    }

    public void Move(bool canMove)
    {
        if (canMove)
        {
            if (canStep && !stepped && PI.horizontal != 0)
            {
                player.position += (Vector3)realStep * Mathf.Sign(PI.horizontal);
                stepped = true;
                AM.SetTrigger(Mathf.Sign(PI.horizontal) == (player.rotation.y == 0 ? 1 : -1) ? "StepForward" : "StepBackward", false);
            }
            else if (Mathf.Abs(PI.horizontal) == 1)
            {
                isRunning = true;
                player.position += Time.fixedDeltaTime * Vector3.right * PI.horizontal * actualVel;
            }
        }

        if (PI.horizontal == 0 || !canMove)
        {
            stepped = false;
            isRunning = false;
        }

        AM.SetBool("isRunning", isRunning);
    }

    public void SetVelocity(float velocity = 0)
    {
        if (velocity == 0)
            actualVel = this.velocity;
        else
            actualVel = velocity;
    }

    public void CanStepWalk(bool value)
    {
        canStep = value;
    }
}
