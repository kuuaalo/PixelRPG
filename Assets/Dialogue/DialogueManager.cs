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
        // Automatically trigger the dialogue when the game starts to get intro-lines
       AutoTriggerDialogue();
       GameEvents.current.onInteractTriggerDay += AutoTriggerDialogue; //After day change autotrigger dialogue again
    }

    // Update is called once per frame
    void Update()
    {
        //If player is in range or isIntroDialogue
        if((isInRange && npcgameObject != null) || GameManager.current.isIntroDialogue) 
        {
            if(Input.GetKeyDown(interactKey)) //and presses a key (E)
            {
                
                if (GameManager.current.isInConversation == false)   //if player isn't in conversation already
                {
                    CheckDialogueStatus();  //check dialogue status
                    PlayAudio playAudio = audioManager.GetComponent<PlayAudio>();
                    playAudio.PlayInteractSound(); //Play Interact-sound
                    
                }else //if already in conversation skip dialogue status check
                {
                    InteractWithNPC(); //Show dialogue test
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
        
        if (npcgameObject != null) //if player has collided with npc 
        {
            npcScript =  npcgameObject.GetComponent<NPC>(); //Get NPC's dialogue script
        }
        else
        {
            npcScript = GetComponent<NPC>(); //Else, use player's own dialogue script for intro-lines
            
        }

        newlines = npcScript.OnConversationStart(); //invoke dialogue function from the script
        InteractWithNPC(); //Invoke NPC-interact function
         

    }

    public void InteractWithNPC()
    {

        DialogueBoxController dialogueBoxController = canvas.GetComponent<DialogueBoxController>(); //Get script from the canvas object
        
        dialogueBoxController.ShowDialogue(newlines); //Summon dialoguebox and give array as parameter
        
       
    }

    public void AutoTriggerDialogue()
    {
        npcgameObject = null; //Set npcgameObject null to avoid overlapping dialogues
        GameManager.current.isIntroDialogue = true; //Set isIntroDialogue to true
        CheckDialogueStatus(); 
    }

}

