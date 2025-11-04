using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector2 posicao_inicial;
    public GamaManager gameManager;
    public Animator anim;
    public Rigidbody2D rb;

    float input_x, input_y = 0;
    public float speed = 7f;
    bool is_walking = false;

    [Header("Vida e Dano")]
    public int vidaMax = 3;
    public int vidaAtual;

    [Header("Knockback")]
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.2f;
    private bool isKnocked = false;

    [Header("Invencibilidade")]
    public float invencibilidadeDuration = 1.5f;
    private bool isInvencivel = false;
    private SpriteRenderer sr;

    private void Start()
    {
        vidaAtual = vidaMax;
        is_walking = false;
        posicao_inicial = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isKnocked) return; 

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

    public void TomarDano(Vector2 origemDano)
    {
        if (isInvencivel) return;

        vidaAtual--;

        if (vidaAtual <= 0)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        Vector2 direcaoKnockback = (transform.position - (Vector3)origemDano).normalized;
        StartCoroutine(KnockbackRoutine(direcaoKnockback));
        StartCoroutine(InvencibilidadeRoutine());
    }

    IEnumerator KnockbackRoutine(Vector2 direcao)
    {
        isKnocked = true;
        rb.linearVelocity = direcao * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }

    IEnumerator InvencibilidadeRoutine()
    {
        isInvencivel = true;
        float elapsed = 0f;
        while (elapsed < invencibilidadeDuration)
        {
            sr.enabled = !sr.enabled; 
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        sr.enabled = true;
        isInvencivel = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 origem = collision.transform.position;
            TomarDano(origem);
        }
    }
}
