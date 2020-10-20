using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    private Animator anim;

    private string actualBoolean;
    private string actualTrigger;

    public AnimationManager(Animator anim)
    {
        this.anim = anim;
    }

    public void SetBool(string boolean, bool value)
    {
        anim.SetBool(boolean, value);        
    }

    public void SetTrigger(string trigger, bool multipleCall = true)
    {
        if (actualTrigger != trigger || !multipleCall)
        {
            actualTrigger = trigger;
            anim.SetTrigger(trigger);
        }
    }

    public void SetInt(string integer, int value)
    {
        anim.SetInteger(integer, value);
        Debug.Log(integer + " " + value);
    }
}
