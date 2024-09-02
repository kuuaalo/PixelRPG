using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{

    public GameObject Customer;
    private CustomerAI customerAI;
    public AudioSource audioSource;
    public GameObject Door;
    public AudioClip doorSound1;
    public AudioClip doorSound2;
    public AudioClip bulbSound;
    private LightControl lightControl;
 

    void Start()
    {
        GameEvents.current.onDayTwoTasks += StartEvent; 

        GameEvents.current.onDoorInteract += DoorEvent;

        lightControl = Door.GetComponent<LightControl>();
        
    }
    
    void StartEvent() //After day 2 tasks finished
    {
        GameManager.current.isInConversation = true; //set player to be in conversation to freeze movement
        
        StartCoroutine(Day2EventCoroutine()); //Start coroutine
    }

    private IEnumerator Day2EventCoroutine()
    {  
        
        audioSource.Play(); //start footstep sound

        yield return new WaitForSeconds(8); //play sound for seconds

        StartCoroutine(DecreaseVolume()); //start the coroutine that decreases the volume

    }

    private IEnumerator DecreaseVolume() //Decreases volume for a smoother cut-off
    {
        while (audioSource.volume > 0)   //While the volume is more than 0
        {
            audioSource.volume -= 0.40f;  //Decrease the volume
            yield return new WaitForSeconds(1); //Wait for a second
        }
        
        audioSource.Stop(); //Stop the audio
        audioSource.volume = 0.8f; //Set volume back to louder
        
        customerAI = Customer.GetComponent<CustomerAI>(); 
        
        customerAI.ChangeState(CustomerAI.NPCState.WalkingToDoor); //Change NPC-state to move towards Door
        
        GameManager.current.isInConversation = false; //Allow player to move again
       
       yield return null;
       
    }

    void DoorEvent() //After interacted with door
    {
        
        StartCoroutine(DoorAudioCoroutine());
    }
    
    private IEnumerator DoorAudioCoroutine()
    {
        GameManager.current.isInConversation = true;
        
        lightControl.CloseLights(); //Turn lights off
        
        Customer.SetActive(false); //Remove customer from scene
        
        audioSource.PlayOneShot(doorSound1); //Play door open sound
        yield return new WaitForSeconds(doorSound1.length + 1); //wait until the sound has finished playing
        
        audioSource.PlayOneShot(doorSound2);
        yield return new WaitForSeconds(doorSound2.length);
        
        lightControl.EventLight(); //Turn on blinking lights
        audioSource.PlayOneShot(bulbSound); //Play blinking light sound
        yield return new WaitForSeconds(3);
        
        lightControl.StopEventLight(); //Stop light blinking
        audioSource.Stop(); //Stop audio
        
        GameManager.current.isInConversation = false;
        
        yield return null;

    }


        
    private void OnDestroy()
    {
        StopAllCoroutines();
        GameEvents.current.onDayTwoTasks -= StartEvent;
        GameEvents.current.onDoorInteract -= DoorEvent;
    }
}
