using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int Damage;
    public float WeaponRange;

    public Transform AttackPoint;
    public LayerMask PlayerLayer;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, WeaponRange, PlayerLayer);

        if(hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-Damage);
        }
    }
}
