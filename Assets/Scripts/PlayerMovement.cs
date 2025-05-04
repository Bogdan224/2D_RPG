using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5.0f;
    public int FacingDirection = 1; 

    public Rigidbody2D rb;
    public Animator anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.velocity = new Vector2(horizontal, vertical) * Speed;
    }

    void Flip()
    {
        FacingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
