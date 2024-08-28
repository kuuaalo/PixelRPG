using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{

    public GameObject Customer;
    private CustomerAI customerAI;
    // Start is called before the first frame update
    void Update()
    {
        

        GameEvents.current.onDoorInteract += StartEvent; //is not getting fired
    }

    // Update is called once per frame
    void StartEvent()
    {
        Debug.Log("Changed npc state");
        customerAI = Customer.GetComponent<CustomerAI>();
        customerAI.ChangeState(CustomerAI.NPCState.WalkingToDoor);
    }
}
