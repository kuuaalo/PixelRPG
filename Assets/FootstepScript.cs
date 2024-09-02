using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepSound;

    void Update()
    {
        //If player presses movement keys, play sound
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.S)  || Input.GetKey(KeyCode.D))
        {
            footstepSound.enabled = true;
        }
        else
        {
            footstepSound.enabled = false;
        }
    }
}
