using System;

public interface IInput
{
    void ReadInput();
    float horizontal { get; }
    float vertical { get; }
    bool jump { get; }
    bool attack { get; }

    event Action OnJump;
    event Action OnAttack;
}
