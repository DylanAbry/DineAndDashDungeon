using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTriggerTracker player = other.GetComponent<PlayerTriggerTracker>();
            if (player != null)
            {
                player.triggersEnteredCount++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTriggerTracker player = other.GetComponent<PlayerTriggerTracker>();
            if (player != null)
            {
                player.triggersEnteredCount--;
            }
        }
    }
}
