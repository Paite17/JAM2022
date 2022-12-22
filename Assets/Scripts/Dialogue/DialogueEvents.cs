using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueEvents 
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPreviousButtonPress;

    public UnityEvent OnPreviousButtonPress => onPreviousButtonPress;
}
