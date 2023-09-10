using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogObject dialogueObject;
    [SerializeField] private PlayerMovement player;

    private Scene currentScene;
    private string sceneName;

    private DialogueEvents[] dialogueEvents;


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "BossRoom")
        {
            Interact(player);
        }
    }
    public void UpdateDialogueObject(DialogObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
        }
    }

    public void AddDialogueEvents(DialogueEvents[] dialogueEvents)
    {
        this.dialogueEvents = dialogueEvents;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovement player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        // BREAKS THE SYSTEM DONT UNCOMMENT FOR NOW
        /*for (int i = 0; i > dialogueEvents.Length; i++)
        {
            // check if event exists
            if (dialogueEvents != null)
            {
                player.DialogueUI.AddDialogueEvents(dialogueEvents.Events);
                dialogueEvents[i].OnPreviousButtonPress?.Invoke();
            }
        } */


        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
