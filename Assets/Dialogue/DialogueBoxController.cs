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
    GameObject npcgameObject;


   
    public void ShowDialogue(string[] newLines, GameObject gameObject)
    {
        dialoguePanel.gameObject.SetActive(true);
        npcgameObject = gameObject;
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
        NPC npcScript =  npcgameObject.GetComponent<NPC>(); 
        npcScript.OnConversationFinish();
        dialoguePanel.gameObject.SetActive(false);
        
    }
}
