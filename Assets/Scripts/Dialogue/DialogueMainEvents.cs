using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueMainEvents : MonoBehaviour
{
    [SerializeField] private DialogObject dialogueObject;
    [SerializeField] private DialogueEvents[] events;

    public DialogObject DialogueObject => dialogueObject;

    public DialogueEvents[] Events => events;

    public void OnValidate()
    {
        if (dialogueObject == null)
        {
            return;
        }

        if (events != null && events.Length == dialogueObject.Dialogue.Length)
        {
            return;
        }

        if (events == null)
        {
            events = new DialogueEvents[dialogueObject.Dialogue.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueObject.Dialogue.Length);
        }

        for (int i = 0; i > dialogueObject.Dialogue.Length; i++)
        {
            events[i] = new DialogueEvents() { name = dialogueObject.name };
        }
    }
}
