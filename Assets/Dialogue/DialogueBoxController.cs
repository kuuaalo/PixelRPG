using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxController : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    private string[] lines;
    private int index = 0;


   
    public void ShowDialogue(string[] newLines)
    {
        dialoguePanel.gameObject.SetActive(true);
        lines = newLines;
        NextLine();
        

    }

    public void NextLine()
    {
        if (index<lines.Length)
        {
            dialogueText.text = lines[index];
            index++;
        }else
        {
            EndDialogue();
        }

    }
    public void EndDialogue()
    {
        dialogueText.text = null;
        index = 0;
        dialoguePanel.gameObject.SetActive(false);
        
    }
}
