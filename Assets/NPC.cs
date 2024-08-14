using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    public bool isInteracted = false;
    public CharacterLines characterLines;
    public GameManager gameManager;
    public bool isLetter = false;
  

    public string[] OnConversationStart()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        
        bool everythingInteracted = gameManager.everythingInteracted;
        string[] lines; 

        if (!everythingInteracted)
        {
          lines = characterLines.lines;
          if (!isInteracted)
          {
            isInteracted = true;
          }

        }else if(isLetter && everythingInteracted)
        {
          lines = characterLines.lines2;
          GameEvents.current.InteractLetter();
        }else 
        {
          lines = characterLines.lines2;
        }
      
      return lines;
    }
}
