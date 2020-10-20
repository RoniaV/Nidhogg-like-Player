using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponSO weapon;

    public WeaponSO w { get { return weapon; } }

    public event Action OnThrow = delegate { };

    private Transform weaponPoint, bonePoint;
    private bool followBone;
    
    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.Sprite;
        GetComponent<BoxCollider2D>().size = weapon.collSize;
    }

    void Update()
    {
        if(weaponPoint != null)
        {
            transform.position = weaponPoint.position;
            transform.rotation = followBone ? bonePoint.rotation : Quaternion.identity;
        }
    }

    public void Throw()
    {
        weaponPoint = null;
        bonePoint = null;
        gameObject.layer = weapon.PickableLayer;
        OnThrow();
    }

    public void Picked(Transform wPoint, Transform bone)
    {
        gameObject.layer = weapon.WeaponLayer;
        weaponPoint = wPoint;
        bonePoint = bone;
    }

    public void FollowBone(bool value)
    {
        followBone = value;
    }
}
