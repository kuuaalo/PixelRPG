using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{   
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index; //private so it can't be modified, avoids naming conflicts
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); //Set gameobject inactive
    }
    void OnEnable() //Interact script sets the object active
    {
        index = 0;
        NextLine();
        Debug.Log("PrintOnEnable:Succesfully activated dialogue");
    }
    
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    void NextLine()
    {
        if (index < lines.Length)
        {
            textComponent.text = lines[index]; //TMP text object is the line at the index
            index++; //index is added to
        }
        else
        {
            textComponent.text = string.Empty; //Text-object is set to empty to enable looping

            gameObject.SetActive(false); //Object set to inactive to hide UI
        }
    }
}

