using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
   
    public bool isInteracted = false;
    public CharacterLines characterLines;
    public GameManager gameManager;
    public bool isLetter = false;
    public bool isPhone = false;
    
  

    public string[] OnConversationStart()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        
        bool everythingInteracted = gameManager.everythingInteracted;
        string[] lines; 

        if (!everythingInteracted) //if player hasn't interacted with everything
        {
          lines = characterLines.lines; //use normal lines
          if (!isInteracted) //if item hasn't been interacted
          {
            isInteracted = true; //set it interacted
          }
          //if object is letter and time can be skipped
          if (isLetter && GameManager.current.canSkip == true) 
          {
          lines = characterLines.lines3; //use secondary lines
          GameEvents.current.InteractLetter(); //Invoke interactletter
          }
        }
        else
        {
          //if everything interacted use lines2
          lines = characterLines.lines2; 
          if(isPhone) //if everything interacted and isphone
          {
            GameManager.current.everythingInteracted = false; //set false to use normal lines
            GameManager.current.canSkip = true; //let letter skip 
          }
        }
      
      return lines;
    }
}
