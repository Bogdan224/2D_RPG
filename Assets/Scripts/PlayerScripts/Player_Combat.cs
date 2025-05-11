using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;

    public Animator anim;

    public void Attack()
    {
        anim.SetBool("isAttacking", true);

       
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.WeaponRange, enemyLayer);
        
        if (enemies.Length > 0)
        {
            Debug.Log(enemies[0].gameObject.name);
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.Damage);
            enemies[0].GetComponent<EnemyKnockback>().Knockback(transform, StatsManager.Instance.KnockbackForce, StatsManager.Instance.StunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.WeaponRange);
    }
}
