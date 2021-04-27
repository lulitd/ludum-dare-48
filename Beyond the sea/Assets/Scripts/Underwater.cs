using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Underwater : MonoBehaviour
{
    public string playerTag;

    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            OnPlayerExit.Invoke();
        }
    }
}
