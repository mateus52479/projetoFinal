using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();
    }

    void move()
    {
        float teclas_horizontais = Input.GetAxis("Horizontal");
        rigid.linearVelocity = new Vector2(teclas_horizontais * speed, rigid.linearVelocity.y);

        float teclas_verticais = Input.GetAxis("Vertical");
        rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, teclas_verticais * speed);

         if (teclas_horizontais > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (teclas_horizontais < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (teclas_horizontais == 0)
        {
        }
        



        if (teclas_verticais > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (teclas_verticais < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (teclas_verticais == 0 )
        {
        }

    }
}
