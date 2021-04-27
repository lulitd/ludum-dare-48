using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    [SerializeField] private string playerTag; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameObject.SetActive(false);
        }
    }
}
