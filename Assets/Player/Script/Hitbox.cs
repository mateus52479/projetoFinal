using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int dano = 1;
    private Transform player;

    void Start()
    {
        player = transform.root;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // INIMIGO NORMAL
        Enemies inimigo = collision.GetComponent<Enemies>();
        if (inimigo != null)
        {
            inimigo.TomarDano(dano, player.position);
            return; // já deu dano, não precisa continuar
        }

        // BOSS
        BossAI boss = collision.GetComponent<BossAI>();
        if (boss != null)
        {
            boss.TomarDano(dano, player.position);
            return;
        }
    }
}
