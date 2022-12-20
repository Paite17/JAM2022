using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DialogueUI/DialogueObject")]

public class DialogObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private AudioSource charVoice;
    [SerializeField] private Sprite charPortrait;
    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public AudioSource CharVoice => charVoice;

    public Sprite CharPortrait => charPortrait;
}