using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Interaction", menuName = "Data/Player Interaction")]
public class PlayerAction : ScriptableObject
{
    public string name;
    public Sprite icon;
    public Color spriteColor; 
}
