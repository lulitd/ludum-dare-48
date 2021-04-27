using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Islander", menuName = "Data/Islander")]
public class IslanderData : ScriptableObject
{
   public string Name;
   public CreatureType Species;
   public Personality personalityType; 
   
   //TODO DIALOG AND QUESTS
}


public enum CreatureType
{
   NONE=0,
   HUMAN,
   CAT, 
   BUNNY,
   BEAR
}

