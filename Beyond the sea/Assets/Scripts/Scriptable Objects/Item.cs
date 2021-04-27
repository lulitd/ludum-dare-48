using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Item", menuName = "Data/Item", order = -10)]
public class Item : ScriptableObject
{
    public string Name;
    public string description; 
}
