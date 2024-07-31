using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource phoneSound;
    

    public void PlaySound()
    {
        phoneSound.Play();
        Debug.Log("RING RING RING");
    }
}
