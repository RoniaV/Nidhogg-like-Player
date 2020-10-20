using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInput : IInput
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public bool jump { get; private set; }
    public bool attack { get; private set; }

    public event Action OnJump = delegate { };
    public event Action OnAttack = delegate { };

    private InputSettings inputSet;

    public PlayerInput(InputSettings inputSet)
    {
        this.inputSet = inputSet;
    }

    public void ReadInput()
    {
        horizontal = Input.GetAxis(inputSet.Horizontal);
        vertical = Input.GetAxis(inputSet.Vertical);

        jump = Input.GetButtonDown(inputSet.Jump);
        if (jump)
        {
            OnJump();
        }
        attack = Input.GetButtonDown(inputSet.Attack);
        if (attack)
            OnAttack();
    }
}
