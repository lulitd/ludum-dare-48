using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower : MonoBehaviour
{
   private bool isActive;
   public Quest radioTrade;

   public GameObject radio;
   public GameObject outline; 
   public bool questComplete = false; 
   private void Start()
   {
      if (DialogPanel.instance != null)
      {
    
         DialogPanel.instance.OnAccept.AddListener(OnAccept);
         DialogPanel.instance.OnDecline.AddListener(OnDeny);
      }
   }

 
   private void OnDisable()
   {
      if (DialogPanel.instance != null)
      {
         DialogPanel.instance.OnAccept.RemoveListener(OnAccept);
         DialogPanel.instance.OnDecline.RemoveListener(OnDeny);
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isActive = true; 
         //show ui
         if (!questComplete)
         {
            DialogPanel.instance.ShowDialog(true, radioTrade.Request + radioTrade.GetTrade(), radioTrade.hasItems());
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         isActive = false; 
         DialogPanel.instance.ShowDialog(false);
         
         if (questComplete)
         GetComponent<Collider>().enabled = false; 
      }
   }
   
   private void OnDeny()
   {
      if (isActive)
      {
         DialogPanel.instance.ShowDialog(true, radioTrade.declineTrade, false);
      }
   }

   private void OnAccept()
   {
      if (isActive)
      {
         DialogPanel.instance.ShowDialog(true, radioTrade.acceptTrade, false);


         var items = radioTrade.trade;
      
         var r = radioTrade.GetRewards();

         foreach (var trade in items)
         {
            PlayerInventory.instance.RemoveFromInventory(trade.item,trade.Amount);
         }
         foreach (var reward in r)
         {
            PlayerInventory.instance.AddToInventory(reward.item,reward.Amount);
         }

         questComplete = true;
         BuildTower();
         

      }
   }

   private void BuildTower()
   {
      radio.SetActive(true);
      outline.SetActive(false);
   }

}
