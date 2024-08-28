using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip interactSound;
    public AudioClip phoneSound;
    private int index;
    
    void Start()
    {
        GameEvents.current.onPhoneCall += EnablePlaySound;
        audioSource.clip = phoneSound; 
       
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
            yield return new WaitForSeconds(audioSource.clip.length); //wait until the clip finishes to play it again
            
            //if player interacts with phone disable sound
            if (GameManager.current.isInConversation && !GameManager.current.everythingInteracted) 
            {
                //phoneSound.enabled = false;
                audioSource.Stop();
            }
            
            
        }
    }

    public void PlaySound()
    {
        audioSource.Play();

    }

    public void PlayInteractSound()
    {
        audioSource.PlayOneShot(interactSound);
    }
    
    private void OnDestroy()
    {
        GameEvents.current.onPhoneCall -= EnablePlaySound;
    }
}
