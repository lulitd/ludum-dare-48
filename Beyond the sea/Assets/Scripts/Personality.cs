using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Personality",menuName = "Data/Personality",order = -100)]
public class Personality : ScriptableObject
{
    public string askMat;
    public string getMat;
    public string denyMat;
    public string[] smallTalk;
    public string talkLeave; 
}

public enum PersonalityType
{
    NONE=0, 
    A,
    B,
    AB,
    O
}