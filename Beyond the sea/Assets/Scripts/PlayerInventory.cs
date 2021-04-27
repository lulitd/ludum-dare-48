using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInventory : MonoBehaviour
{
  public static PlayerInventory instance; 
  
  public Dictionary<Item, Tuple<int, RectTransform> > CurrentInventory;
  public RectTransform UIPanel;
  public GameObject itemUI; 

  private void OnEnable()
  {
    if (instance != null && instance != this)
    {
      Destroy(this);
      return;
    }

    if (instance == null)
    {
      instance = this; 
      
    }
    CurrentInventory  = new Dictionary<Item,Tuple<int,RectTransform>>();
  }

  private void OnDestroy()
  {
    instance = null; 
  }

  public void AddToInventory(Item item, int amount = 1)
  {
    if (CurrentInventory.TryGetValue(item, out var currentValue))
    {
      // yay, value exists!
      var newAmount = amount + currentValue.Item1;
      var text = currentValue.Item2.gameObject.GetComponentInChildren<TextMeshProUGUI>();
      if (newAmount==1) text.text = item.name;
      else if (newAmount>1) text.text = $"{item.name} x{newAmount}";
      CurrentInventory[item] = new Tuple<int, RectTransform>(newAmount,currentValue.Item2);
    }
    else
    {
      var g = GameObject.Instantiate(itemUI, UIPanel);
      var rect = g.GetComponent<RectTransform>();
      var text = g.GetComponentInChildren<TextMeshProUGUI>();
      if (amount==1) text.text = item.name;
      else if (amount>1) text.text = $"{item.name} x{amount}";
      CurrentInventory.Add(item,new Tuple<int, RectTransform>(amount,rect));
    }
    
  }
  
  public void RemoveFromInventory(Item item, int amount = 1)
  {
    if (CurrentInventory.TryGetValue(item, out var currentValue))
    {
      // yay, value exists!
      if (currentValue.Item1 >=amount)
      {

        var newAmount =  currentValue.Item1-amount ;
      var text = currentValue.Item2.gameObject.GetComponentInChildren<TextMeshProUGUI>();
      
      if (newAmount == 1) text.text = item.name;
      else text.text = $"{item.name} x{newAmount}"; 
      
      CurrentInventory[item] = new Tuple<int, RectTransform>(newAmount, currentValue.Item2);
    }
  }
  
    
  }
}
