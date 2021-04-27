using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingItem : MonoBehaviour
{
    public string playerTag = "Player";
    public Item item; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            //TODO: ADD TO INVENTORY
            
            PlayerInventory.instance?.AddToInventory(item);
            gameObject.SetActive(false);
        }
    }
}
