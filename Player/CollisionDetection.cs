using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private int weaponLayer;
    [SerializeField] private int kickLayer;
    [SerializeField] private int pickableLayer;
    
    public List<GameObject> insideObjects;

    public delegate void WeaponDelegate(Weapon weapon);

    public event WeaponDelegate WeaponCollided;
    public event Action KickCollided = delegate { };
    public event WeaponDelegate PickableCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        insideObjects.Add(collision.gameObject);

        if (collision.gameObject.layer == weaponLayer)
            WeaponCollided(collision.gameObject.GetComponent<Weapon>());
        else if (collision.gameObject.layer == kickLayer)
            KickCollided();
        else if (collision.gameObject.layer == pickableLayer)
            PickableCollision(collision.gameObject.GetComponent<Weapon>());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == weaponLayer)
            WeaponCollided(collision.gameObject.GetComponent<Weapon>());
        else if (collision.gameObject.layer == kickLayer)
            KickCollided();
        else if (collision.gameObject.layer == pickableLayer)
            PickableCollision(collision.gameObject.GetComponent<Weapon>());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        insideObjects.Remove(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        insideObjects.Add(collision.gameObject);

        if (collision.gameObject.layer == weaponLayer)
            WeaponCollided(collision.gameObject.GetComponent<Weapon>());
        else if (collision.gameObject.layer == kickLayer)
            KickCollided();
        else if (collision.gameObject.layer == pickableLayer)
            PickableCollision(collision.gameObject.GetComponent<Weapon>());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == weaponLayer)
            WeaponCollided(collision.gameObject.GetComponent<Weapon>());
        else if (collision.gameObject.layer == kickLayer)
            KickCollided();
        else if (collision.gameObject.layer == pickableLayer)
            PickableCollision(collision.gameObject.GetComponent<Weapon>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideObjects.Remove(collision.gameObject);
    }
}
