using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   public GameObject blackOutSquare;

    void Start()
    {
        //When player clicks yes on letter and day changes
        GameEvents.current.onInteractTriggerDay += EnableDaySquare;

    }

   
    public void EnableDaySquare()
    {
        StartCoroutine(DaySquare());
    }

    private IEnumerator DaySquare()
    {
        blackOutSquare.gameObject.SetActive(true); 
        yield return new WaitForSeconds(4); //display 'day 2' text and black screen for 4 seconds
        blackOutSquare.gameObject.SetActive(false); //close black screen
    }

        private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= EnableDaySquare;
    }

}

