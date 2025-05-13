using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime)
    {
        enemyMovement.ChangeState(EnemyMovement.EnemyState.Knockback);
        StartCoroutine(KnockbackTimer(knockbackTime));
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
    }

    private IEnumerator KnockbackTimer(float knockbackTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        enemyMovement.ChangeState(EnemyMovement.EnemyState.Idle);
    }
}
