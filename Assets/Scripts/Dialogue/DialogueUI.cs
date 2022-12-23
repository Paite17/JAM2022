using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private Image character;

    

    public bool isOpen { get; private set; }

    // only realised now that i mispelt responseHandler as resonseHandler and right now i can't be bothered to fix it
    // should probably fix it at some point in the future though, it might get confusing
    private ResonseHandler responseHandler;
    private DialogueActivator dialogueActivator;
    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResonseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogObject dialogueObject)
    {
        isOpen = true; 
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }


    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    public void AddDialogueEvents(DialogueEvents[] dialogueEvents)
    {
        dialogueActivator.AddDialogueEvents(dialogueEvents);
    }

    private IEnumerator StepThroughDialogue(DialogObject dialogueObject)
    { 
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            

            string dialogue = dialogueObject.Dialogue[i];

            AudioClip voice = dialogueObject.CharVoice[i];

            Sprite charPortrait = dialogueObject.CharPortrait[i];

            yield return RunTypingEffect(dialogue, voice);

            textLabel.text = dialogue;

            
            if (dialogueObject.CharPortrait != null)
            {
                character.sprite = charPortrait;
            }
            else
            {
                character.gameObject.SetActive(false);
            }

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            }

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            // it keeps getting worse
            if (dialogueObject.Dialogue.Length == 10)
            {
                // scripted thingie test
                if (dialogueObject.Dialogue[9] == "NO REASON")
                {
                    Debug.Log("WHOA its the funny dialogue!!!!");
                    GameObject crackElfObj = GameObject.Find("CrackElf");
                    ElfFunny crack = crackElfObj.GetComponent<ElfFunny>();

                    crack.RunIntoWall();
                }
            }
            else if (dialogueObject.Dialogue.Length == 5)
            {
                if (dialogueObject.Dialogue[4] == "Correctamundo! Head right on in!")
                {
                    Debug.Log("Removing the wall...");
                    GameObject wallObj = GameObject.Find("Quirky");
                    Quirky quirky = wallObj.GetComponent<Quirky>();

                    quirky.RemoveWall();
                }
            }

           CloseDialogueBox();
        }

        
    }

    private IEnumerator RunTypingEffect(string dialogue, AudioClip voice)
    {
        typewriterEffect.Run(dialogue, textLabel, voice);

        while (typewriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        isOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
