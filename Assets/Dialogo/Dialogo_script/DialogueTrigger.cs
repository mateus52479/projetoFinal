using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("NPC")]
    public string npcName;
    public Sprite npcPortrait;

    [Header("Falas")]
    [TextArea(2, 5)]
    public string[] sentences;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            DialogueSystem.instance.StartDialogue(
                npcName,
                npcPortrait,
                sentences
            );
        }
    }
}
