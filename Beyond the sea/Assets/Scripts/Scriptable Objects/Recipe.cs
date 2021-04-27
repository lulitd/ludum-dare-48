using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe", order = -10)]
public class Recipe : ScriptableObject
{
    public Item Result;
    public List<IngredientParts> RequiredIngredients;
    
    public bool IsAbleToCreateItem(IEnumerable<IngredientParts> inventory)
    {
        var status = RequiredIngredients
            .Select(requiredAmount =>
                (from items in inventory
                    where items.item == requiredAmount.item
                    select items.amount >= requiredAmount.amount).FirstOrDefault()).All(hasEnough => hasEnough);

        Debug.Log($"Able to create item ${status}");
        return status; 
    }

}

[Serializable]
public class IngredientParts
{
    public Item item;
    public int amount;
}

