using UnityEngine;

// https://youtu.be/K9hJTO583_Y?si=QPAakjw8uRsZII83

public class Npc_dialogo : MonoBehaviour
{
    public Sprite profile;
    public string speechTxt;
    public string actorName;

    public LayerMask playerLayer;

    private Dialogo dc;

    private void Start()
    {
        dc = FindAnyObjectByType<Dialogo>();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,  playerLayer);

        if (hit != null) 
        { 
            dc.speech(profile, speechTxt, actorName);
        }
    }

}
