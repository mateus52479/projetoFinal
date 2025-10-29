using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float speed;
   public Rigidbody2D rb;
   public Vector2 mov;
   public Animator anim;

    void Update()
    {
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", mov.x);
        anim.SetFloat("Vertical", mov.y);
        anim.SetFloat("Speed", mov.sqrMagnitude);

        mov.Normalize();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + mov * speed * Time.fixedDeltaTime);
    }
}
