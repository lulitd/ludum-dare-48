using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoints : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
           other.gameObject.GetComponent<UnderwaterPlayerController>().OnAbleToSwim(true, this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.gameObject.GetComponent<UnderwaterPlayerController>().OnAbleToSwim(false);
        }
    }
}
