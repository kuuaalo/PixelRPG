using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public enum NPCState
    {
    Idle,
    Following,
    Fleeing
    }
    
    public NPCState currentState = NPCState.Idle;
    public Transform player;
    public float followDistance = 4f;
    public float fleeDistance = 3f;
    public float moveSpeed = 0.5f;
    public float stopDistance = 4f;
     

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
        }
    }

    void IdleState()
    {
        NPC npcScript = GetComponent<NPC>();
        if(npcScript.isInteracted == true && GameManager.current.isInConversation == false)
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

    public void ChangeState(NPCState newState)
    {
        currentState = newState;
        
    }
}
