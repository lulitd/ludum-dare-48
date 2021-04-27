using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float currentDivingHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isDiving; 


    public void GoDiving()
    {
        currentDivingHealth = maxHealth;
        
        
    }
    
}
