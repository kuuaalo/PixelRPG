using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{   

    public static GameManager current;

    public int currentDay;
    public bool everythingInteracted;
    public bool phoneCallListened;
    public bool canSkip;
    
    public GameObject phone;
    public GameObject window;
    public GameObject desk;
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
        npcScript3 = desk.GetComponent<NPC>();
        GameEvents.current.onInteractTriggerDay += onClickYes;
    }
    

    void Update()
    {
        if (!everythingInteracted && !phoneCallListened)
        {
            InteractedCheck();
        }//else if(!isInConversation) //if everythinginteracted and player is not in conversation trigger event
        
        DayCheck();
 
    }

    void InteractedCheck() 
    {
        if (npcScript1 != null && npcScript2 != null && npcScript3 != null)
        {
            bool phone_isInteracted = npcScript1.isInteracted;
            bool window_isInteracted = npcScript2.isInteracted;
            bool desk_isInteracted = npcScript3.isInteracted;
                
            if (phone_isInteracted && window_isInteracted  && desk_isInteracted) //If all of these are interacted
            {
                GameEvents.current.PhoneCall(); //Trigger phone call
                
                everythingInteracted = true; //set everythinginteracted true, 
                phoneCallListened = true;
                Debug.Log("Phone call listened and everything interacted");
            }
        }
        
    }

    void DayCheck()
    {
        
        if (currentDay == 1)
        {
            customer.SetActive(true);
            
        }
    }

    void onClickYes() //if player clicks yes on the letter adds to the day counter
    {
        currentDay ++;
    }
    
    private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= onClickYes;
    }
}
