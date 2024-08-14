using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   public GameObject blackOutSquare;

    void Start()
    {
        GameEvents.current.onInteractTriggerDay += EnableDaySquare;
    }

   
    public void EnableDaySquare()
    {
        StartCoroutine(DaySquare());
    }

    private IEnumerator DaySquare()
    {
        blackOutSquare.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        blackOutSquare.gameObject.SetActive(false);
    }

        private void OnDestroy()
    {
        GameEvents.current.onInteractTriggerDay -= EnableDaySquare;
    }

}

