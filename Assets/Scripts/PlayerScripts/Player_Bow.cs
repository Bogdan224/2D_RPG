using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;


    private Vector2 aimDirection = Vector2.right;

    private float shootCooldawn = .5f;
    private float shootTimer;

    private bool isPrepareToShoot = false;
    private bool isReadyToShoot = false;

    private Animator anim;
    private PlayerMovement playerMovement;


    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            HandleAiming();

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            

            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                isReadyToShoot = false;
                isPrepareToShoot = true;
            }
            if (isPrepareToShoot)
            {
                if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    isReadyToShoot = true;
                }
            } 
        }
    }

    public void Attack()
    {
        anim.SetBool("isAttacking", true);
        playerMovement.enabled = false;
        playerMovement.rb.velocity = Vector2.zero;
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
        playerMovement.enabled = true;
        
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }

    public async void Shoot()
    {
        if (shootTimer <= 0)
        {
            anim.speed = 0;
            await Task.Run(() => 
            {
                while (!isReadyToShoot)
                {
                    
                }
                
            });
            anim.speed = 1;

            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDirection;
            shootTimer = shootCooldawn;
            isReadyToShoot = false;
            isPrepareToShoot = false;
        }

    }


}
