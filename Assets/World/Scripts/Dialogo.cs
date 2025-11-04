using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    [Header("componentes")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("configurações")]
    public float typingSpeed;

    public void speech(Sprite p, string txt, string actorName)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        speechText.text = txt;
        actorNameText.text = actorName;

    }

}
