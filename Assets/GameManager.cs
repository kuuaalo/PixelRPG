using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{   

    public static GameManager current;

    public int currentDay = 1;
    public bool everythingInteracted;
    
    public GameObject phone;
    public GameObject window;
    public GameObject desk;

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
        if (!everythingInteracted)
        {
            StoryEvent();
        }else if(!isInConversation)
        {
            GameEvents.current.PhoneCall();
        }
 
    }

    void StoryEvent() 
    {
        if (npcScript1 != null && npcScript2 != null && npcScript3 != null)
        {
            bool phone_isInteracted = npcScript1.isInteracted;
            bool window_isInteracted = npcScript2.isInteracted;
            bool desk_isInteracted = npcScript3.isInteracted;
                
            if (phone_isInteracted && window_isInteracted  && desk_isInteracted)
            {
                everythingInteracted = true; 
                  
            }
        }
        
    }

    void onClickYes()
    {
        currentDay ++;
        Debug.Log(currentDay);
    }
    
    private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= onClickYes;
    }
}
