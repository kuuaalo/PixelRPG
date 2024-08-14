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

    public event Action onInteractTriggerDay;
    public void InteractTriggerDay()
    {
        if(onInteractTriggerDay != null)
        {
            onInteractTriggerDay();
        }
    }

    public event Action onInteractLetter;
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
}
