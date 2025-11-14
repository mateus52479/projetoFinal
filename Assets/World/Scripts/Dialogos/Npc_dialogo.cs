using UnityEngine;

// https://youtu.be/K9hJTO583_Y?si=QPAakjw8uRsZII83

public class Npc_dialogo : MonoBehaviour
{
    public Sprite profile;
    public string speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radious;

    private Dialogo dc;

    private void Start()
    {
        dc = FindAnyObjectByType<Dialogo>();
    }

    public void FixedUpdate()
    {
        Interact();
    }
    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null) 
        { 
            dc.speech(profile, speechTxt, actorName);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }

}
