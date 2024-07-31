using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{   
    public int currentDay;
    public bool everythingInteracted;
    
    public GameObject phone;
    public GameObject window;
    public GameObject desk;

    private NPC npcScript1;
    private NPC npcScript2;
    private NPC npcScript3;

    public UnityEvent phoneCall;

     private bool eventTriggered;

    
    void Start()
    {
        npcScript1 = phone.GetComponent<NPC>();
        npcScript2 = window.GetComponent<NPC>();
        npcScript3 = desk.GetComponent<NPC>();

       
    }
    

    void Update()
    {

        

        if (npcScript1 != null && npcScript2 != null && npcScript3 != null)
        {
            bool phone_isInteracted = npcScript1.isInteracted;
            bool window_isInteracted = npcScript2.isInteracted;
            bool desk_isInteracted = npcScript3.isInteracted;
            
            if (phone_isInteracted && window_isInteracted  && desk_isInteracted)
            {
                everythingInteracted = true;
                Debug.Log("Set everything interacted");
                if (!eventTriggered)
                {
                    StoryEvent();
                    eventTriggered = true;
                }
            }
        }
 
    }

    void StoryEvent() 
    {
        phoneCall.Invoke();
    }
}
