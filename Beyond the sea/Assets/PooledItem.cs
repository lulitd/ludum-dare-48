using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledItem : MonoBehaviour
{

   private ItemSpawner pool; 
   public void SetPool(ItemSpawner pool)
   {
      this.pool = pool; 
   }
   private void OnDisable()
   {
      if (pool != null)
      {
         pool.DeactiveObject(this.gameObject);
      }
   }
}
