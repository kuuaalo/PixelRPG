using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    private CharacterLines lines;
    public GameObject npcgameObject;
    public Canvas canvas;
    private bool isInteracted;
    public GameObject gameManagerObject;
    public string[] newlines;
    public GameObject audioManager;
    
    void Start()
    {
        // Automatically trigger the dialogue when the game starts
       AutoTriggerDialogue();
       GameEvents.current.onInteractTriggerDay += AutoTriggerDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        if((isInRange && npcgameObject != null) || GameManager.current.isIntroDialogue) //If player is in range
        {
            if(Input.GetKeyDown(interactKey)) //and presses a key
            {
                
                if (GameManager.current.isInConversation == false)   //if player isn't in conversation already
                {
                    CheckDialogueStatus();  //check dialogue status
                    PlayAudio playAudio = audioManager.GetComponent<PlayAudio>();
                    playAudio.PlayInteractSound(); //Play Interact-sound
                    
                }else //skip check
                {
                    InteractWithNPC();
                }
            }
        }  

        if(GameManager.current.isInConversation == false)
        {
            GameManager.current.isIntroDialogue = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) //Object enters collider
    {
        
        if(collision.gameObject.CompareTag("Interactable")) //If object is tagged 'interactable', bool is true
        {
            isInRange = true;
            npcgameObject = collision.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Object leaves collider area
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            isInRange = false; 
            npcgameObject = null;
        }
    } 


    public void CheckDialogueStatus()
    {
        NPC npcScript;
        
        if (npcgameObject != null)
        {
            npcScript =  npcgameObject.GetComponent<NPC>();
        }
        else
        {
            npcScript = GetComponent<NPC>();
            
        }

        newlines = npcScript.OnConversationStart();
        InteractWithNPC();
        

    }

    public void InteractWithNPC()
    {

        DialogueBoxController dialogueBoxController = canvas.GetComponent<DialogueBoxController>(); //Get script from the canvas object
        
        dialogueBoxController.ShowDialogue(newlines); //Summon method and give array as parameter
        
       
    }

    public void AutoTriggerDialogue()
    {
        npcgameObject = null;
        GameManager.current.isIntroDialogue = true;
        CheckDialogueStatus();
    }

}

