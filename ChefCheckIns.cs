using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class ChefCheckIns : MonoBehaviour
{
    [SerializeField] public Animator redCheckIn = null;
    [SerializeField] public Animator jumpscare = null;
    [SerializeField] public Animator fadeIn = null;
    public AudioSource jumpScareAudio;
    public AudioSource heartBeat;
    public AudioSource door;
    public bool playerInTrigger;
    public string redCheckInName;
    private bool isCheckInPlaying;
    private bool isCheckInComplete;
    private bool shouldTriggerJumpscare;
    private bool isGracePeriodActive; // New flag to track grace period

    public ScannerWarningManager scannerScript;
    public PlayerInteraction piScript;
    public PanelManagementBeginning pmbScript;
    public PauseGame pauseScript;
    public PlayerMovement pmScript;
    public PlayerCam pcScript;
    public Timer timerScript;

    public GameObject jumpScareBackground;

    void Start()
    {
        playerInTrigger = false;  // Start with player outside the trigger
        isCheckInPlaying = false;
        isCheckInComplete = false;
        shouldTriggerJumpscare = false;
        isGracePeriodActive = false; // Grace period starts inactive

        ScannerWarningManager.Instance.OnCheckInWarning += HandleCheckInWarning;

        piScript.enabled = true;
        pcScript.enabled = true;
        pmScript.enabled = true;
        timerScript.enabled = true;

        jumpScareBackground.SetActive(false);
    }

    void Update()
    {
        // Update the state of the check-in animation
        AnimatorStateInfo currentStateInfo = redCheckIn.GetCurrentAnimatorStateInfo(0);
        isCheckInPlaying = currentStateInfo.IsName(redCheckInName);
        isCheckInComplete = currentStateInfo.normalizedTime >= 1.0f && isCheckInPlaying;

        // Trigger jumpscare if player leaves during check-in animation
        if ((shouldTriggerJumpscare && isCheckInPlaying && !playerInTrigger && !isCheckInComplete) || (isCheckInPlaying && !playerInTrigger && !isCheckInComplete))
        {
            TriggerJumpscare();
            shouldTriggerJumpscare = false; // Prevent re-triggering jumpscare
        }

        if (!heartBeat.isPlaying && isGracePeriodActive && !playerInTrigger)
        {
            heartBeat.Play();
        }
        if (playerInTrigger && isGracePeriodActive && heartBeat.isPlaying)
        {
            heartBeat.Stop();
        }
    }

    void HandleCheckInWarning(int checkInType)
    {
        if (checkInType == 1) // 1 for red light (Chef check-in)
        {
            StartCoroutine(GracePeriod());
        }
    }

    private IEnumerator GracePeriod()
    {
        isGracePeriodActive = true; // Start grace period

        if (!playerInTrigger && !heartBeat.isPlaying)
        {
            heartBeat.Play(); // Start heartbeat if player is outside the trigger
        }

        yield return new WaitForSeconds(10f);

        if (isGracePeriodActive)
        {
            isGracePeriodActive = false; // Grace period ends
            heartBeat.Stop(); // Stop heartbeat after grace period ends

            // Start the check-in animation after grace period ends
            redCheckIn.Play("CheckIn", 0, 0.0f);
            door.Play();
            isCheckInPlaying = true;
            isCheckInComplete = false;
            yield return new WaitForSeconds(9.5f);
            door.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            shouldTriggerJumpscare = false; // Reset flag when player re-enters
            heartBeat.Stop(); // Stop heartbeat if player re-enters trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;

            // Set the flag to allow jumpscare when player leaves during check-in animation
            if (isCheckInPlaying && !isCheckInComplete)
            {
                shouldTriggerJumpscare = true;
            }

            // Play heartbeat only if the player is out of trigger during the grace period
            if (isGracePeriodActive && !heartBeat.isPlaying)
            {
                heartBeat.Play();
            }
        }
    }

    private void TriggerJumpscare()
    {
        if (!jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Jumpscare"))
        {
            StopCoroutine(GracePeriod());
            if(pmbScript.safeSparkle.isPlaying)
            {
                pmbScript.safeSparkle.Stop();
            }
            door.enabled = false;
            jumpscare.Play("Jumpscare");
            heartBeat.Stop();
            jumpScareAudio.Play();
            jumpScareBackground.SetActive(true);
            pcScript.enabled = false;
            pauseScript.enabled = false;
            pmbScript.enabled = false;
            pmScript.enabled = false;
            piScript.enabled = false;
            timerScript.enabled = false;
            StartCoroutine(WaitForJumpscareToEnd());
        }
    }

    private IEnumerator WaitForJumpscareToEnd()
    {
        yield return new WaitForSeconds(jumpscare.GetCurrentAnimatorStateInfo(0).length + 3f);

        if (fadeIn != null)
        {
            fadeIn.Play("FadeIn");
            yield return new WaitForSeconds(fadeIn.GetCurrentAnimatorStateInfo(0).length);
        }

        SceneManager.LoadScene("DeathEndingScene");
    }

    public void CancelCheckIn()
    {
        if (isGracePeriodActive) // Only allow canceling during grace period
        {
            isGracePeriodActive = false; // End grace period
            heartBeat.Stop(); // Stop heartbeat if it's playing

            // Reset the animator and check-in flags
            redCheckIn.ResetTrigger("CheckIn");
            isCheckInPlaying = false;
            isCheckInComplete = false;

            scannerScript.redLight.SetActive(false);
        }
    }
}
