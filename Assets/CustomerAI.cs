using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public enum NPCState //states for customer NPC
    {
    Idle,
    Following,
    Fleeing,
    WalkingToDoor
    }
    
    public NPCState currentState = NPCState.Idle; //default state
    public Transform player;    //player location on scene
    public Transform door;  //door location on scene
    public float followDistance = 4f;
    public float fleeDistance = 3f;
    public float moveSpeed = 0.5f;
    public float stopDistance = 4f;
    public float doorReachThreshold = 0.5f; 
     

    public void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                
                IdleState();
                break;

            case NPCState.Following:
                
                FollowPlayer();
                break;

            case NPCState.Fleeing:
                
                FleeFromPlayer();
                break;
            
            case NPCState.WalkingToDoor:
                
                WalkToDoor();
                break;
        }
    }

    void IdleState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        NPC npcScript = GetComponent<NPC>();
        
        //after player interacts with the NPC and moves far enough, start follow behaviour
        if(npcScript.isInteracted == true && GameManager.current.isInConversation == false && distanceToPlayer >= followDistance)
        {
            ChangeState(NPCState.Following);
        }
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //if NPC reaches close enough to the player, stop movement
        if (distanceToPlayer <= stopDistance && distanceToPlayer > fleeDistance)
        {
            Debug.Log("NPC stopped");
        }
        
        //if player is far enough continue movement
        else if (distanceToPlayer > stopDistance && distanceToPlayer > fleeDistance)
        {
            Vector2 direction  = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        
        //if player comes too close, change to flee state
        else
        {
            ChangeState(NPCState.Fleeing);
        }

    }

    void FleeFromPlayer()
    {

        Vector2 direction = (transform.position - player.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
        
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //if player goes far enough go back to follow state
        if (distanceToPlayer > followDistance)
        {
            ChangeState(NPCState.Following);
        }
    }

    void WalkToDoor() //triggered from outside the script, during scripted event
    {
        moveSpeed = 2f; //change speed faster
        
        float distanceToDoor = Vector2.Distance(transform.position, door.position);
        
        if (distanceToDoor <= doorReachThreshold) //check if player is close enough to door and stop
        {
            
        }else
        {
            //move npc closer to door
            Vector2 direction = (door.position - transform.position).normalized; 
            transform.position = Vector2.MoveTowards(transform.position, door.position, moveSpeed * Time.deltaTime); 
            
        }
    }

    public void ChangeState(NPCState newState) //takes new state as parameter
    {
        currentState = newState; //changes state
        
    }
}
