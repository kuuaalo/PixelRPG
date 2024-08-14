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
        //StartCoroutine(PlaySoundCoroutine());
        PlaySound();
    }

    private IEnumerator PlaySoundCoroutine()
    {   
        yield return new WaitForSeconds(2);

        if (GameManager.current.isInConversation == false)
        {
            PlaySound(); //enable audio source
            yield return new WaitForSeconds(phoneSound.clip.length);
        } else 
        {

        phoneSound.enabled = false; //disable sound
        }
    }

    public void PlaySound()
    {
        phoneSound.enabled = true; //enable audio source
    }
}
