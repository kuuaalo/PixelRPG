using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    public bool isInteracted = false;
    public CharacterLines characterLines;
    public GameManager gameManager;
    
    public void OnConversationFinish()
    {
       if (!isInteracted)
        {
          isInteracted = true;
        }
    }
}
