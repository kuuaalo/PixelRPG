using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionDialogUI : MonoBehaviour
{
    private TextMeshProUGUI TextMeshPro;
    private Button yesBtn;
    private Button noBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() 
    {
        TextMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();
        
        ShowQuestion("Do you want to do this", () => {
            Debug.Log("Yes");
        }, () => {
            Debug.Log("No");
        
        });
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction) {
        TextMeshPro.text = questionText;
        yesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
        noBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));

    }
}
