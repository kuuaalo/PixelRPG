using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    public bool isInteracted = false;
    public CharacterLines characterLines;
    public GameManager gameManager;
    public bool isLetter = false;
    public bool isPhone = false;
    public bool isDoor = false;
    
  

    public string[] OnConversationStart()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        
        bool everythingInteracted = gameManager.everythingInteracted;
        string[] lines; 
        
        CharacterLines.DayDialogue dayDialogue = GetDayDialogue();

        if (!everythingInteracted) //if player hasn't interacted with everything
        {
          lines = dayDialogue.lines1; //use normal lines
          if (isDoor) 
          {
          Debug.Log("Is Door");
          lines = dayDialogue.lines1; //use first lines
          GameEvents.current.DoorInteract(); //Invoke doorinteract
          }
          if (!isInteracted) //if item hasn't been interacted
          {
            isInteracted = true; //set it interacted
          }
          //if object is letter and time can be skipped
          if (isLetter && GameManager.current.canSkip == true) 
          {
          lines = dayDialogue.lines3; //use secondary lines
          GameEvents.current.InteractLetter(); //Invoke interactletter
          }
        }
        else
        {
          //if everything interacted use lines2
          lines = dayDialogue.lines2; 
          if(isPhone) //if everything interacted and isphone
          {
            GameManager.current.everythingInteracted = false; //set false to use normal lines
            GameManager.current.canSkip = true; //let letter skip 
          }
        }
      return lines;
    }
    
    private CharacterLines.DayDialogue GetDayDialogue()
    {
      switch(GameManager.current.currentDay)
      {
        case 0: return characterLines.day1;
        case 1: return characterLines.day2;
        default: return null;
      }
    }
}
