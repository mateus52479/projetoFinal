using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitbox; 
    public float tempoAtivo = 0.2f; 
    public float tempoRecarga = 0.4f; 

    private bool podeAtacar = true;
    private Animator anim;
    private Player player; 

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

        if (hitbox != null)
            hitbox.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Z) && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    IEnumerator Atacar()
    {
        podeAtacar = false;
        anim.SetTrigger("attack"); 

        
        if (hitbox != null)
            hitbox.SetActive(true);

        yield return new WaitForSeconds(tempoAtivo);

        if (hitbox != null)
            hitbox.SetActive(false);

        yield return new WaitForSeconds(tempoRecarga);
        podeAtacar = true;
    }
}
