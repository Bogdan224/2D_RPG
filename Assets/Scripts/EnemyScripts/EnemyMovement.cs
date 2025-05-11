using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    public float playerDetectRange = 5;
    public float attackRange = 1.25f;
    public float attackCooldown = 1;

    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float facingDirection;
    
    private float attackCooldownTimer;

    private EnemyState enemyState;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ChangeState(EnemyState.Idle);
        facingDirection = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();

            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        } 

    }

    void Chase()
    {
        
        if (player.position.x > transform.position.x && facingDirection == -1
                || player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * Speed;
    }

    public void ChangeState(EnemyState state)
    {
        //Exit the current animation
        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool("isIdle", false);
                break;
            case EnemyState.Chasing:
                animator.SetBool("isChasing", false);
                break;
            case EnemyState.Attacking:
                animator.SetBool("isAttacking", false);
                break;
            case EnemyState.Knockback:
                animator.SetBool("isKnockedback", false);
                break;
            default:
                break;
        }

        //Update current state
        enemyState = state;

        //Update the new animation
        switch (enemyState)
        {
            case EnemyState.Idle:
                animator.SetBool("isIdle", true);
                break;
            case EnemyState.Chasing:
                animator.SetBool("isChasing", true);
                break;
            case EnemyState.Attacking:
                animator.SetBool("isAttacking", true);
                break;
            case EnemyState.Knockback:
                animator.SetBool("isKnockedback", true);
                break;
            default:
                break;
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if(hits.Length > 0)
        {
            player = hits[0].transform;

            //if the player in attack range AND cooldown is ready
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange
            && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking) 
            {
                ChangeState(EnemyState.Chasing);
            } 
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    public enum EnemyState
    {
        Idle, Chasing, Attacking, Knockback
    }
}
