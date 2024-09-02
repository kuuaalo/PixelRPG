using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    public bool isInteracted = false;
    public CharacterLines characterLines;
    public GameManager gameManager;
    public bool isLetter = false;
    public bool isFinalTask = false;
    
    delegate void TaskDelegate();
    TaskDelegate finaltask;
  
    void Start()
    {
        GameEvents.current.onInteractTriggerDay += onClickYes;
    }

    public string[] OnConversationStart() //Called after player interacts with object, returns string array
    {
        gameManager = gameManager.GetComponent<GameManager>();
        
        bool everythingInteracted = gameManager.everythingInteracted;
        string[] lines; 
        
        CharacterLines.DayDialogue dayDialogue = GetDayDialogue(); //Call function to retrieve appropriate dialogue

        if (!everythingInteracted) //if player hasn't interacted with everything
        {
          lines = dayDialogue.lines1; //use normal lines

          if (!isInteracted) //if item hasn't been interacted
          {
            isInteracted = true; //set it interacted
          }
          //if object is letter and time can be skipped
          if (isLetter && GameManager.current.lastTaskFinished == true) 
          {
          lines = dayDialogue.lines3; //use third lines
          GameEvents.current.InteractLetter(); //Invoke interactletter
          
          }
        }
        else
        {
          lines = dayDialogue.lines2; //if everything interacted use lines2

          GetDayTask(); //Returns delegate variable function

          if (finaltask != null)
          {
            finaltask(); //Useless delegate, but wanted to try it out
            if(isFinalTask) //Checks if the object is this day's final task
            {
            GameManager.current.everythingInteracted = false; //set false to use normal lines
            GameManager.current.lastTaskFinished = true; //let letter skip day
            }
          }
        }
        
      return lines; //Return lines to dialoguemanager
    }
    
    private CharacterLines.DayDialogue GetDayDialogue()
    {
      switch(GameManager.current.currentDay) //Gets day number from gamemanager
      {
        case 0: return characterLines.day1; //Returns correct day's voicelines based on it
        case 1: return characterLines.day2;
        default: return null;
      }
    }

    private void GetDayTask()
    {
      switch(GameManager.current.currentDay) //Gets day number from gamemanager
      {
        case 0:
          finaltask = PhoneCall; //Returns correct day's final task based on it
          break;
        case 1: 
          finaltask = DoorOpen;
          break;
        default: 
          finaltask = null;
          break;
      }
    }

    void PhoneCall()
    {
      if(gameObject.name == "Phone") // If object's name is phone it is the final task
      {
        isFinalTask = true;
      }
    }

    void DoorOpen()
    {
      if(gameObject.name == "Door") // If object's name is door it is the final task
      {
        isFinalTask = true;
      }
      
    }

    private void onClickYes()
    {
      isInteracted = false; //reset interacted checks on day change
      isFinalTask = false; //reset finaltask checks on day change
    }

    private void OnDestroy()
    {
      GameEvents.current.onInteractTriggerDay -= onClickYes;
    }

}
