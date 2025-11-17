using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class HeartSystem : MonoBehaviour
{
    Player player;
    public int vida;
    public int vidaMax;

    public bool isDead;
    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic()
    {
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
                coracao[i].sprite = cheio;
            else
                coracao[i].sprite = vazio;

            if (i < vidaMax)
                coracao[i].enabled = true;
            else
                coracao[i].enabled = false;
        }
    }

    void DeadState()
    {
        if (vida <= 0 && !isDead)
        {
            isDead = true;

            // animação de morte
            player.anim.SetBool("isDead", true);

            // impede o jogador de se mover
            GetComponent<Player>().enabled = false;

            // inicia coroutine para trocar de cena
            StartCoroutine(LoadMenu());
        }
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(2f); // espera a animação terminar

        SceneManager.LoadScene("Menu"); // coloque o nome da sua cena aqui
    }
}
