using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource phoneSound;
    private int index;
    
    void Start()
    {
        GameEvents.current.onPhoneCall += EnablePlaySound;
    }

    public void EnablePlaySound()
    {
        StartCoroutine(PlaySoundCoroutine());

    }

    private IEnumerator PlaySoundCoroutine()
    {   
        yield return new WaitForSeconds(3);

        while (!GameManager.current.isInConversation) //Play sound until player enters conversation
        {
            PlaySound(); //enable audio source
            yield return new WaitForSeconds(phoneSound.clip.length); //wait until the clip finishes to play it again
            
            //if player interacts with phone disable sound
            if (GameManager.current.isInConversation && !GameManager.current.everythingInteracted) 
            {
                phoneSound.enabled = false;
            }
            
            
        }
    }

    public void PlaySound()
    {
        phoneSound.enabled = true; //enable audio source
    }
    
    private void OnDestroy()
    {
        GameEvents.current.onPhoneCall -= EnablePlaySound;
    }
}
