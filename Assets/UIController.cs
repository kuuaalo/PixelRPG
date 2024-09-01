using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public GameObject blackOutSquare;
    [SerializeField] TextMeshProUGUI dayText;
    

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
        int dayNumber = GameManager.current.currentDay + 2; //get day number and 2 bc im lazy

        if (dayNumber == 3)
        {
            dayText.text = ("GAME END (TBC?)");
        }
        else{
        dayText.text = ("DAY " + dayNumber);
        yield return new WaitForSeconds(4); //display text and black screen for 4 seconds
        blackOutSquare.gameObject.SetActive(false); //close black screen
        }
        
    }

        private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= EnableDaySquare;
    }

}

