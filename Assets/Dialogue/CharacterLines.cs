using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterLines : ScriptableObject
{
    [System.Serializable]
    public class DayDialogue //nested class that's used create instances
    {
        public string[] lines1;
        public string[] lines2; 
        public string[] lines3; 
    }

    //create an instance of the nested class for every day
    public DayDialogue day1; 
    public DayDialogue day2;
    public DayDialogue day3;
    public DayDialogue day4;
    public DayDialogue day5;
    public DayDialogue day6;
    public DayDialogue day7;
    
}
