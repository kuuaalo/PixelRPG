using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{   
    public GameObject gameObjectToActivate;
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange) //If player is in range
        {
            if(Input.GetKeyDown(interactKey)) //and presses a key
            {
                interactAction.Invoke(); //trigger event
            }
        }  
    }
    private void OnTriggerEnter2D(Collider2D collision) //Object enters collider
    {
        if(collision.gameObject.CompareTag("Player")) //If object is tagged 'player', bool is true
        {
            isInRange = true;
            Debug.Log("Player now in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Object leaves collider area
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player not now in range");
        }
    }
}
