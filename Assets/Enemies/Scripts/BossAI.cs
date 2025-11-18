using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class BossAI : MonoBehaviour
{
    // =============================
    //        ESTADOS DO BOSS  
    // =============================
    public BossState currentState = BossState.Idle;

    [Header("AI do Boss")]
    public float idleTime = 2f;
    public float warningDuration = 1f;
    public float dashSpeed = 12f;
    public float recoverTime = 1.5f;

    public float stepBackDistance = 0.2f;
    public int stepBackCount = 4;

    private Vector2 lastPlayerPos;
    private bool isDashing = false;

    // =============================
    //        VIDA DO BOSS
    // =============================
    [Header("Vida do Boss")]
    public int vidaMax = 20;
    public int vidaAtual;

    [Header("Knockback")]
    public float knockbackForce = 5f;

    [Header("Referências")]
    public Slider barraVida;

    // =============================
    //     COMPONENTES INTERNOS
    // =============================
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private bool podeMover = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Vida
        vidaAtual = vidaMax;
        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaAtual;
        }

        // Inicia comportamento
        StartCoroutine(IdleState());
    }

    void Update()
    {
        if (vidaAtual <= 0)
        {
            Morrer();
            return;

        }
    }

    // ============================================================
    //                       LÓGICA DE ESTADOS
    // ============================================================

    IEnumerator IdleState()
    {
        if (!podeMover) yield break;

        currentState = BossState.Idle;

        yield return new WaitForSeconds(idleTime);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            lastPlayerPos = player.transform.position;

        StartCoroutine(WarningState());
    }

    IEnumerator WarningState()
    {
        if (!podeMover) yield break;

        currentState = BossState.Warning;

        for (int i = 0; i < stepBackCount; i++)
        {
            transform.position -= transform.right * stepBackDistance;

            sr.color = Color.white;
            yield return new WaitForSeconds(0.08f);

            sr.color = Color.red;
            yield return new WaitForSeconds(0.08f);
        }

        StartCoroutine(DashState());
    }

    IEnumerator DashState()
    {
        if (!podeMover) yield break;

        currentState = BossState.Dashing;
        isDashing = true;

        Vector2 direction = (lastPlayerPos - (Vector2)transform.position).normalized;

        float time = 0f;
        float dashDuration = 0.7f;

        while (time < dashDuration)
        {
            if (!podeMover) break;

            rb.linearVelocity = direction * dashSpeed;
            time += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        StartCoroutine(RecoverState());
    }

    IEnumerator RecoverState()
    {
        if (!podeMover) yield break;

        currentState = BossState.Recover;

        yield return new WaitForSeconds(recoverTime);

        StartCoroutine(IdleState());
    }

    // ============================================================
    //                   SISTEMA DE VIDA / DANO
    // ============================================================

    public void TomarDano(int dano, Vector2 origemAtaque)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);

        AtualizarBarraVida();

        Vector2 dir = (transform.position - (Vector3)origemAtaque).normalized;
        StartCoroutine(Knockback(dir));

        if (vidaAtual <= 0)
        {
            Morrer();
            SceneManager.LoadScene("Fim");
        }
    }

    IEnumerator Knockback(Vector2 dir)
    {
        podeMover = false;
        rb.linearVelocity = dir * knockbackForce;

        yield return new WaitForSeconds(0.2f);

        rb.linearVelocity = Vector2.zero;
        podeMover = true;
    }

    // ============================================================
    //                          MORTE
    // ============================================================

    void Morrer()
    {
        rb.linearVelocity = Vector2.zero;
        podeMover = false;

        if (barraVida != null)
            barraVida.gameObject.SetActive(false);

        Destroy(gameObject, 1.0f);
    }

    // ============================================================
    //                   BARRA DE VIDA UI
    // ============================================================

    void AtualizarBarraVida()
    {
        if (barraVida != null)
            barraVida.value = vidaAtual;
    }
}

public enum BossState
{
    Idle,
    Warning,
    Dashing,
    Recover
}
