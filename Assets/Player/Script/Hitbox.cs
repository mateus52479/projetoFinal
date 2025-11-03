using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int dano = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // tenta acessar um script de inimigo com método "TomarDano"
            collision.SendMessage("TomarDano", dano, SendMessageOptions.DontRequireReceiver);
        }
    }
}
