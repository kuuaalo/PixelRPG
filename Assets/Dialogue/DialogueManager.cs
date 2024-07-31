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
    int index;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

            Debug.Log("Player now in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Object leaves collider area
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            isInRange = false; 
            
            Debug.Log("Player not now in range");
        }
    } 


    public void CheckDialogueStatus()
    {
        NPC npcScript =  npcgameObject.GetComponent<NPC>(); 
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        bool everythingInteracted = gameManager.everythingInteracted;

        CharacterLines characterLines = npcScript.characterLines; //Get characterLines variable from the NPC-script
        string[] lines;

        if (!everythingInteracted)
        {
            
            lines = characterLines.lines;
            
            
            
        }else
        {
            Debug.Log("That shouldn't have happened");
            lines = characterLines.lines2;
        }

        InteractWithNPC(lines);
    }

    public void InteractWithNPC(string[] lines)
    {

        DialogueBoxController dialogueBoxController = canvas.GetComponent<DialogueBoxController>(); //Get script from the canvas object
        
        dialogueBoxController.ShowDialogue(lines); //Summon method and give lines array as parameter
        NPC npcScript =  npcgameObject.GetComponent<NPC>(); 
        npcScript.OnConversationFinish();
       

    }
}

