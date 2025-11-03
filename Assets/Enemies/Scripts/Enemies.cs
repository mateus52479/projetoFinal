using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GamaManager gameManager;
    public int vida = 3;

    void Update()
    {
        if (vida <= 0)
        {
            Morrer();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.perdeVidas(1);
        }
    }

    // chamado quando o hitbox encosta
    public void TomarDano(int dano)
    {
        vida -= dano;
        Debug.Log(name + " levou dano! Vida restante: " + vida);
    }

    void Morrer()
    {
        // aqui você pode colocar animação de morte, som, partículas, etc.
        Destroy(gameObject);
    }
}
