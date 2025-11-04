using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
        DeadState();
    }

    void HealthLogic() { 
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }


        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }



            if (i < vidaMax)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if (vida <= 0)
        {
            isDead = true;
            player.anim.SetBool("isDead", isDead);
            GetComponent<Player>().enabled = false;
            Destroy(gameObject, 2.0f);
        }
    }


}
