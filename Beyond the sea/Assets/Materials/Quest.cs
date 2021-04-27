using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Quest",menuName = "Data/Quest",order = -100)]
public class Quest : ScriptableObject
{

    public string Request;
    public string acceptTrade;
    public string declineTrade;
    public List<QuestAmount> trade;

    public List<QuestAmount> reward;
    public bool randomSelection = true;
    public bool repeatable = true;
    public bool isComplete = false; 
    
    public string GetTrade()
    {
        string tradeString = "<br>";
        int i = 1; 
        foreach (var item in trade)
        {
            tradeString += $"{item.item.name} x{item.Amount} ";
            i++;
            if (i % 2 == 0)
            {
                tradeString += "<br>";
            }
        }
        return tradeString; 
    }
    
    

    public List<QuestAmount> GetRewards()
    {
        List<QuestAmount> r;


        if (randomSelection)
        {
            r = new List<QuestAmount> {reward[Random.Range(0, reward.Count)]};
        }
        else
        {
           r =  new List<QuestAmount>(reward);
        }

        
        return r;
    } 

    public bool hasItems()
    {

        var hasItems = true;

        foreach (var item in trade)
        {
            if (PlayerInventory.instance.CurrentInventory.TryGetValue(item.item,out var value))
            {
                if (item.Amount > value.Item1)
                {
                    hasItems = false; 
                    break;
                    
                }
            }
            else
            {
                hasItems = false;
                break;
            }
        }

        return hasItems;

    }
}

[System.Serializable]
public struct QuestAmount
{
    public Item item;
    public int Amount; 
}

