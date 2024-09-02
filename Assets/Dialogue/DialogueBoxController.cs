using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxController : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject questionDialog;
    private string[] lines;
    private int index = 0;
    public GameObject player;
    
    

    void Start()
    {
        GameEvents.current.onInteractLetter += onInteractLetterItem; //when player interacts with letter open question dialog
        GameEvents.current.onInteractTriggerDay += EndDialogue; //when player skips day call end dialogue
    }

   
    public void ShowDialogue(string[] newLines) //Takes array from dialoguemanager as parameter
    {
        GameManager.current.isInConversation = true; //tells game manager that player is in the middle of conversation
        dialoguePanel.gameObject.SetActive(true); //shows dialogue box
        lines = newLines;
        NextLine();
    }

    public void NextLine()
    {
        if (index<lines.Length) //if index is smaller than array's length
        {
            dialogueText.text = lines[index];  //show the text inside array
            index++; //Add to index
        }else
        {
            EndDialogue(); //call function
        }

    }
    public void EndDialogue()
    {
        dialogueText.text = null; //reset dialogue text object
        
        index = 0; //set index back to 0
        
        GameManager.current.isInConversation = false; //player no longer in conversation
        
        dialoguePanel.gameObject.SetActive(false); //hide dialogue panel
        
        questionDialog.gameObject.SetActive(false); //hide question dialog
        
    }
    
    private void onInteractLetterItem()
    {
        questionDialog.gameObject.SetActive(true); // show question dialog if player interacts with letter
    }


    private void OnDestroy()
    {
        GameEvents.current.onInteractLetter -= onInteractLetterItem;
        GameEvents.current.onInteractTriggerDay -= EndDialogue;
    }
}
