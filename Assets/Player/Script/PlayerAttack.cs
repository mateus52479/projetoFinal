using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitbox; // arraste o objeto Hitbox aqui
    public float tempoAtivo = 0.2f; // tempo que o hitbox fica ativo
    public float tempoRecarga = 0.4f; // tempo até poder atacar de novo

    private bool podeAtacar = true;
    private Animator anim;
    private Player player; // referência pro script Player (já existente)

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

        if (hitbox != null)
            hitbox.SetActive(false);
    }

    void Update()
    {
        // ataque com Z, igual ao seu código
        if (Input.GetKeyDown(KeyCode.Z) && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;
        anim.SetTrigger("attack"); // usa teu trigger atual

        // ativa hitbox por um curto tempo
        if (hitbox != null)
            hitbox.SetActive(true);

        yield return new WaitForSeconds(tempoAtivo);

        if (hitbox != null)
            hitbox.SetActive(false);

        yield return new WaitForSeconds(tempoRecarga);
        podeAtacar = true;
    }
}
