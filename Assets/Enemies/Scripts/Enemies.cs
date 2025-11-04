using System.Collections;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int vida = 3;
    public int dano = 1;
    public float distanciaAtaque = 0.5f;
    public float knockbackForce = 5f;
    public LayerMask obstaculos; 

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private bool podeMover = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (vida <= 0)
        {
            Morrer();
            return;
        }

        if (player != null && podeMover)
        {
            MoverAtePlayer();
        }
    }

    void MoverAtePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        // Verifica se há obstáculo à frente 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.6f, obstaculos);
        if (hit.collider != null)
        {
            // Tenta desviar levemente pro lado
            dir += new Vector2(dir.y, -dir.x) * 0.5f;
            dir.Normalize();
        }

        // Movimento suave
        Vector2 novaVel = dir * moveSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, novaVel, 0.1f);

        // Atualiza animações
        anim.SetFloat("Speed", rb.linearVelocity.magnitude);

        // Flip do sprite (vira o lado)
        if (dir.x != 0)
        {
            sr.flipX = dir.x < 0;
        }
    }

    public void TomarDano(int dano, Vector2 origemAtaque)
    {
        vida -= dano;
        anim.SetTrigger("Hurt");

        // Knockback
        Vector2 knockDir = (transform.position - (Vector3)origemAtaque).normalized;
        StartCoroutine(Knockback(knockDir));

        if (vida <= 0)
        {
            Morrer();
        }
    }

    IEnumerator Knockback(Vector2 dir)
    {
        podeMover = false;
        rb.linearVelocity = dir * knockbackForce;
        yield return new WaitForSeconds(0.2f);
        podeMover = true;
    }

    void Morrer()
    {
        anim.SetTrigger("Dead");
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inimigo causou dano ao player!");
        }
    }
}
