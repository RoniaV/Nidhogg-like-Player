using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicked 
{
    public bool isKicked { get; private set; }

    private IInput PI;
    private CollisionDetection CD;
    private AnimationManager AM;

    private int kickedFrames;

    private bool getUp;
    private int actualFrames;

    public Kicked(IInput pi, CollisionDetection cd, PlayerSettings PS, AnimationManager am)
    {
        PI = pi;
        CD = cd;
        AM = am;

        kickedFrames = PS.KickedFrames;

        CD.KickCollided += Kicking;
    }

    public void Kicking()
    {
        if (!isKicked)
        {
            isKicked = true;
            AM.SetTrigger("Kick", false);
        }
    }

    public void GetUp()
    {
        actualFrames++;
            Debug.Log("kick frames: " + actualFrames);

        if (PI.vertical == 1 && isKicked)
        {
            Debug.Log("press up");
            if(actualFrames >= kickedFrames)
            {
                Debug.Log("GetUp");
                isKicked = false;
                actualFrames = 0;
                AM.SetTrigger("GetUp", false);
            }
        }
    }
}
