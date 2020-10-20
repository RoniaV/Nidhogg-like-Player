using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    bool hitting { get; }

    void Hit();
    void ExitHit();
}
