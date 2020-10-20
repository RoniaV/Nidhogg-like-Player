using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/MovSettings", fileName = "PlayerMovData")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float velocity;
    [SerializeField] private float step;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 crouchSize;
    [SerializeField] private float crouchVelocity;
    [SerializeField] private float rollLenght;
    [SerializeField] private int inputController;
    [SerializeField] private InputSettings inputSet;
    [SerializeField] private int kickedFrames;

    public float Velocity { get { return velocity; } }
    public float Step { get { return step; } }
    public float JumpForce { get { return jumpForce; } }
    public LayerMask GroundLayer { get { return groundLayer; } }
    public Vector2 CrouchSize { get { return crouchSize; } }
    public float CrouchVelocity { get { return crouchVelocity; } }
    public float RollLenght { get { return rollLenght; } }
    public int InputController { get { return inputController; } }
    public InputSettings InputSet { get { return inputSet; } }
    public int KickedFrames { get { return kickedFrames; } }
}
