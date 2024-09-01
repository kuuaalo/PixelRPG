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

    // Update is called once per frame
    
    void StartEvent()
    {
        GameManager.current.isInConversation = true;
        Debug.Log("Changed npc state");
        
        StartCoroutine(Day2EventCoroutine());
    }

    private IEnumerator Day2EventCoroutine()
    {   
        Debug.Log("Doesn't work here");
        
        audioSource.Play(); //start footstep sound

        yield return new WaitForSeconds(8); //play it for seconds

        StartCoroutine(DecreaseVolume()); //start the coroutine that decreases the volume

    }

    private IEnumerator DecreaseVolume()
    {
        while (audioSource.volume >0)   //While the volume is more than 0
        {
            audioSource.volume -= 0.40f;  //Decrease the volume
            yield return new WaitForSeconds(1); //Wait for a second
        }
        audioSource.Stop(); //Stop the audio
        
        audioSource.volume = 0.8f;
        customerAI = Customer.GetComponent<CustomerAI>();
        customerAI.ChangeState(CustomerAI.NPCState.WalkingToDoor);
        GameManager.current.isInConversation = false;
       yield return null;
       
    }

    void DoorEvent()
    {
        Debug.Log("Started event");
        Customer.SetActive(false);
        StartCoroutine(DoorAudioCoroutine());
    }
    
    private IEnumerator DoorAudioCoroutine()
    {
        GameManager.current.isInConversation = true;
        lightControl.CloseLights();
        audioSource.PlayOneShot(doorSound1);
        yield return new WaitForSeconds(doorSound1.length + 1);
        audioSource.PlayOneShot(doorSound2);
        yield return new WaitForSeconds(doorSound2.length);
        lightControl.EventLight();
        audioSource.PlayOneShot(bulbSound);
        yield return new WaitForSeconds(3);
        lightControl.StopEventLight();
        audioSource.Stop();
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
