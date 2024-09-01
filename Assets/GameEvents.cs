using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onInteractTriggerDay; //When player chooses yes on letter and skips to next day
    public void InteractTriggerDay()
    {
        if(onInteractTriggerDay != null)
        {
            onInteractTriggerDay();
        }
    }

    public event Action onInteractLetter; //When player interacts with the letter
    public void InteractLetter()
    {
         if(onInteractLetter != null)
        {
            onInteractLetter();
        }
    }

    public event Action onPhoneCall; 
    public void PhoneCall()
    {
         if(onPhoneCall != null)
        {
            onPhoneCall();
        }
    }
    
    public event Action onDayTwoTasks; 
    public void DayTwoTasks()
    {
         if(onDayTwoTasks!= null)
        {
            onDayTwoTasks();
        }
    }

    public event Action onDoorInteract; 
    public void DoorInteract()
    {
         if(onDoorInteract!= null)
        {
            onDoorInteract();
        }
    }

}
