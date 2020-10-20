using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/FightSettings", fileName = "PlayerFightSettings")]
public class PlayerFightSettings : ScriptableObject
{
    [SerializeField] private int playerLayer;
    [SerializeField] private int hitLayer;
    [SerializeField] private float punchStep;
    [SerializeField] private Vector2 downKickSize;
    [SerializeField] private Vector2 airKickSize;
    [SerializeField] private int downKickFrames;
    [SerializeField] private int punchFrames;
    [SerializeField] private LayerMask airKickLayers;
    [SerializeField] private float airKickForce;
    [SerializeField] private float weaponMov;
    [SerializeField] private int throwWeaponFrames;

    public int PlayerLayer { get { return playerLayer; } }
    public int HitLayer { get { return hitLayer; } }
    public float PunchStep { get { return punchStep; } }
    public Vector2 DownKickSize { get { return downKickSize; } }
    public Vector2 AirKickSize { get { return airKickSize; } }
    public int DownKickFrames { get { return downKickFrames; } }
    public int PunchFrames { get { return punchFrames; } }
    public int AirKickLayers { get { return airKickLayers; } }
    public float AirKickForce { get { return airKickForce; } }
    public float WeaponMov { get { return weaponMov; } }
    public int ThrowWeaponFrames { get { return throwWeaponFrames; } }
}
