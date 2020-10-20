using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private int weaponLayer;
    [SerializeField] private int pickableLayer;
    [SerializeField] private int minPos;
    [SerializeField] private int maxPos;
    [SerializeField] private float step;
    [SerializeField] private int weaponFrames;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 colliderSize;

    public int WeaponLayer { get { return weaponLayer; } }
    public int PickableLayer { get { return pickableLayer; } }
    public int MinPos { get { return minPos; } }
    public int MaxPos { get { return maxPos; } }
    public float Step { get { return step; } }
    public int WeaponFrames { get { return weaponFrames; } }
    public Sprite Sprite { get { return sprite; } }
    public Vector2 collSize { get { return colliderSize; } }
}
