using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerTracker : MonoBehaviour
{
    public int triggersEnteredCount = 0;  // Track number of trigger zones the player is in
    public Animator jumpscare;
    public Animator orangeCheckIn;
    private bool isCheckInPlaying;

    void Update()
    {
        if (triggersEnteredCount == 0 && isCheckInPlaying)
        {
            TriggerJumpscare();
        }
    }

    private void TriggerJumpscare()
    {
        jumpscare.Play("Jumpscare");
        // Disable player controls or handle jumpscare logic
    }

    public void StartCheckIn()
    {
        orangeCheckIn.Play("CheckInAnimation");
        isCheckInPlaying = true;
    }

    public void EndCheckIn()
    {
        isCheckInPlaying = false;
    }
}
