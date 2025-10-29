using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    float input_x, input_y = 0;
    public float speed = 7f;
    bool is_walking = false;

    private void Start()
    {
        is_walking = false;
    }

    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        is_walking = (input_x != 0 || input_y != 0);

        if (is_walking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            anim.SetFloat("input_x", input_x);
            anim.SetFloat("input_y", input_y);
        }

        anim.SetBool("is_walking", is_walking);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("attack");
        }
    }
}
