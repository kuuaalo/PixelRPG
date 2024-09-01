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

    public string[] OnConversationStart()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        
        bool everythingInteracted = gameManager.everythingInteracted;
        string[] lines; 
        
        CharacterLines.DayDialogue dayDialogue = GetDayDialogue();

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
          lines = dayDialogue.lines3; //use secondary lines
          GameEvents.current.InteractLetter(); //Invoke interactletter
          
          }
        }
        else
        {
          lines = dayDialogue.lines2; //if everything interacted use lines2

          GetDayTask();

          if (finaltask != null)
          {
            finaltask();
            if(isFinalTask)
            {
            GameManager.current.everythingInteracted = false; //set false to use normal lines
            GameManager.current.lastTaskFinished = true; //let letter skip day
            }
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

    private void GetDayTask()
    {
      switch(GameManager.current.currentDay)
      {
        case 0:
          finaltask = PhoneCall;
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
      if(gameObject.name == "Phone")
      {
        isFinalTask = true;
      }
    }

    void DoorOpen()
    {
      if(gameObject.name == "Door")
      {
        isFinalTask = true;
      }
      
    }

    private void onClickYes()
    {
      isInteracted = false; //reset interacted checks on day change
    }

    private void OnDestroy()
    {
      GameEvents.current.onInteractTriggerDay -= onClickYes;
    }

}
