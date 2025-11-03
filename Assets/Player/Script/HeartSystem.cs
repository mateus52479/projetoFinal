using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int vida;
    public int vidaMax;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthLogic();
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
}
