using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    [Header("UI")]
    public GameObject dialogueUI;
    public Text nameText;
    public Text speechText;
    public Image portraitImage;

    [Header("Configurações")]
    public float typingSpeed = 0.03f;
    public AudioSource typeSound;

    private string[] sentences;
    private int index;
    private bool isTyping = false;
    private bool isDialogueActive = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                // Termina a frase instantaneamente
                StopAllCoroutines();
                speechText.text = sentences[index];
                isTyping = false;
            }
            else
            {
                NextSentence();
            }
        }
    }

    // Início do diálogo
    public void StartDialogue(string npcName, Sprite portrait, string[] lines)
    {
        nameText.text = npcName;
        portraitImage.sprite = portrait;

        sentences = lines;
        index = 0;

        dialogueUI.SetActive(true);
        isDialogueActive = true;

        // Impedir player de se mover
        Time.timeScale = 0;

        StartCoroutine(TypeSentence());
    }

    // Digitação letra por letra
    IEnumerator TypeSentence()
    {
        isTyping = true;
        speechText.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;

            if (typeSound != null)
                typeSound.Play();

            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    // Ir para próxima fala
    void NextSentence()
    {
        index++;

        if (index < sentences.Length)
        {
            StartCoroutine(TypeSentence());
        }
        else
        {
            EndDialogue();
        }
    }

    // Finalizar diálogo
    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;

        // Destrava player
        Time.timeScale = 1;
    }
}
