using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public enum NPCState
    {
    Idle,
    Following,
    Fleeing,
    WalkingToDoor
    }
    
    public NPCState currentState = NPCState.Idle;
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
        if(npcScript.isInteracted == true && GameManager.current.isInConversation == false && distanceToPlayer >= followDistance)
        {
            ChangeState(NPCState.Following);
        }
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= stopDistance && distanceToPlayer > fleeDistance)
        {
            Debug.Log("NPC stopped");
        }
        
        else if (distanceToPlayer > stopDistance && distanceToPlayer > fleeDistance)
        {
            Vector2 direction  = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        
        
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
        if (distanceToPlayer > followDistance)
        {
            ChangeState(NPCState.Following);
        }
    }

    void WalkToDoor()
    {
        moveSpeed = 2f;
        float distanceToDoor = Vector2.Distance(transform.position, door.position);
        if (distanceToDoor <= doorReachThreshold) // Check if NPC has reached the door
        {
            
        }else
        {
            Vector2 direction = (door.position - transform.position).normalized; // Direction towards the door
            transform.position = Vector2.MoveTowards(transform.position, door.position, moveSpeed * Time.deltaTime);
            
        }
    }

    public void ChangeState(NPCState newState)
    {
        currentState = newState;
        
    }
}
