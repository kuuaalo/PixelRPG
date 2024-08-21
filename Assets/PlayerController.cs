using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   Rigidbody2D body;
   SpriteRenderer sr;
   float horizontal;
   float vertical;
   public float runSpeed = 20.0f;
   

   void Start ()
   {
      body = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      GameEvents.current.onInteractTriggerDay += onDayChange;
      
   }

   void Update()
   {
      if (GameManager.current.isInConversation == false)
         {
         body.constraints = RigidbodyConstraints2D.FreezeRotation;
         // Gives a value between -1 and 1
         horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
         vertical = Input.GetAxisRaw("Vertical"); // -1 is down

         if (horizontal != 0 && horizontal > 0)
         {
         sr.flipX = true;
         }
         else if (horizontal != 0 && horizontal < 0)
         {
         sr.flipX = false;
         }
      }
      else //if player is in conversation freeze movement
      {
         body.constraints = RigidbodyConstraints2D.FreezeAll;
      }
   }

   void FixedUpdate()
   {
      body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
   }
   
   void onDayChange()
    {
      //reset player position when the day changes
      transform.position = new Vector2(-9,-1);
    }
    
    private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= onDayChange;
    }
}
