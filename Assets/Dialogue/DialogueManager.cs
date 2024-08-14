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
    
    


    // Update is called once per frame
    void Update()
    {
        if(isInRange && npcgameObject != null) //If player is in range
        {
            if(Input.GetKeyDown(interactKey)) //and presses a key
            {
                Debug.Log("Player pressed button");
                
                CheckDialogueStatus();
            }
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
            
        }
    } 


    public void CheckDialogueStatus()
    {
        NPC npcScript =  npcgameObject.GetComponent<NPC>(); 
        newlines = npcScript.OnConversationStart();
        InteractWithNPC();

    }

    public void InteractWithNPC()
    {

        DialogueBoxController dialogueBoxController = canvas.GetComponent<DialogueBoxController>(); //Get script from the canvas object
        
        dialogueBoxController.ShowDialogue(newlines); //Summon method and give array as parameter
       
    }
}

