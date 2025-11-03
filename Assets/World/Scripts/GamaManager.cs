using UnityEngine;
using TMPro;

public class GamaManager : MonoBehaviour
{
    public int vidas = 3;

    public void perdeVidas(int vida)
    {
        vidas -= vida;

        if (vidas <= 0)
        {
            vidas = 0;
            Time.timeScale = 0;
        }
        Debug.Log("vidas: " + vidas);

        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().reiniciar_posicao();

    }


}
