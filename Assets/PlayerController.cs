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
      
   }

   void Update()
   {
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

   void FixedUpdate()
   {
      body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
   }
}
