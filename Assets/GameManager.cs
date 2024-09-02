using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{   

    public static GameManager current;

    public int currentDay;
    public bool everythingInteracted;
    public bool lastTaskFinished;
    public bool tasksFinished;
    private bool DayeventTriggered;
    public bool isIntroDialogue = false;
    
    public GameObject phone;
    public GameObject window;
    public GameObject coffee;
    public GameObject customer;

    private NPC npcScript1;
    private NPC npcScript2;
    private NPC npcScript3;


    private bool eventTriggered;

    public bool isInConversation;

    private void Awake()
    {
        current = this;
    }
    
    void Start()
    {
        npcScript1 = phone.GetComponent<NPC>();
        npcScript2 = window.GetComponent<NPC>();
        npcScript3 = coffee.GetComponent<NPC>();
        GameEvents.current.onInteractTriggerDay += onClickYes;
        
    }
    

    void Update()
    {
        //if player isn't in dialogue and hasn't finished all tasks, check to play audio
        if (!everythingInteracted && !tasksFinished && !isInConversation) 
        {
            InteractedCheck();
        }

        //If player has done the last task and event hasn't been triggered
        if (lastTaskFinished && !DayeventTriggered && !isInConversation)
        {
            GetDayEvent();
        }
    
    }

    void InteractedCheck() 
    {
        if (npcScript1 != null && npcScript2 != null && npcScript3 != null)
        {
            bool phone_isInteracted = npcScript1.isInteracted;
            bool window_isInteracted = npcScript2.isInteracted;
            bool coffee_isInteracted = npcScript3.isInteracted;
                
            if (phone_isInteracted && window_isInteracted  && coffee_isInteracted) //If all of these are interacted
            {
                GetDaySound(); //Call and invoke action
                
                everythingInteracted = true; //set everythinginteracted true, 
                
                tasksFinished = true; //set all tasks finished
                
            }
        }
        
    }

    void onClickYes() //if player clicks yes on the letter
    {
        currentDay ++;  //adds to the day counter
        everythingInteracted = false; //sets all checks back to false
        tasksFinished = false; 
        lastTaskFinished = false;
        isIntroDialogue = false;
        DayeventTriggered = false;
        DayCheck();
    }

    void DayCheck()
    {
        
        if (currentDay == 1) //if second day, add npc
        {
            customer.SetActive(true);
            
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= onClickYes;
    }

    private void GetDaySound() 
    {
      switch(currentDay) //Call function based on day
      {
        case 0:
          GameEvents.current.PhoneCall(); 
          break;
        case 1: 
        GameEvents.current.DayTwoTasks();
          break;
      }
    }

    private void GetDayEvent()
    {
        switch(currentDay) //Call function based on day
         {
            case 0:
            break;
            case 1: 
            GameEvents.current.DoorInteract();
            break;
        }
        DayeventTriggered = true;
        
    }
}
