using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog",menuName = "Data/Dialog", order = -100)]
public class Dialog: ScriptableObject
{
    public string[] lines; 
}
