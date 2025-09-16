using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class MildCheckIn : MonoBehaviour
{
    [SerializeField] public Animator orangeCheckIn = null;
    [SerializeField] public Animator fadeIn = null;
    [SerializeField] public Animator jumpscare = null;
    public AudioSource heartBeat;
    public AudioSource jumpScareAudio;
    public AudioSource door;
    public string orangeCheckInName;
    public bool isCheckInPlaying;
    public bool isCheckInComplete;
    private bool shouldTriggerJumpscare;
    public bool isGracePeriodActive;

    public GameObject jumpScareBackground;

    public PanelManagementBeginning pmbScript;
    public ScannerWarningManager scannerScript;
    public PauseGame pauseScript;
    public PlayerInteraction piScript;
    public PlayerMovement pmScript;
    public PlayerCam pcScript;
    public Timer timerScript;
    public FridgeInteractable fridgeScript;
    public OvenInteractable ovenScript;

    private PlayerTriggerTracker triggerTracker;

    void Start()
    {
        triggerTracker = GetComponent<PlayerTriggerTracker>();
        isCheckInPlaying = false;
        isCheckInComplete = false;
        shouldTriggerJumpscare = false;
        isGracePeriodActive = false;

        ScannerWarningManager.Instance.OnCheckInWarning += HandleCheckInWarning;

        piScript.enabled = true;
        pcScript.enabled = true;
        pmScript.enabled = true;
        timerScript.enabled = true;

        jumpScareBackground.SetActive(false);

    }

    void Update()
    {
        AnimatorStateInfo currentStateInfo = orangeCheckIn.GetCurrentAnimatorStateInfo(0);
        isCheckInPlaying = currentStateInfo.IsName(orangeCheckInName);
        isCheckInComplete = currentStateInfo.normalizedTime >= 1.0f && isCheckInPlaying;

        if ((shouldTriggerJumpscare && isCheckInPlaying && triggerTracker.triggersEnteredCount == 0 && !isCheckInComplete) || (isCheckInPlaying && triggerTracker.triggersEnteredCount == 0 && !isCheckInComplete)
            || (isCheckInPlaying && !isCheckInComplete && (ovenScript.ovenInteracted == true || fridgeScript.fridgeInteracted == true)))
        {
            TriggerJumpscare();
            shouldTriggerJumpscare = false; // Prevent re-triggering jumpscare
        }

        // Trigger heartbeat only when player is outside the orange trigger space
        if (triggerTracker.triggersEnteredCount == 0 && isGracePeriodActive && !heartBeat.isPlaying)
        {
            heartBeat.Play();
        }
        if (triggerTracker.triggersEnteredCount > 0 && isGracePeriodActive && heartBeat.isPlaying)
        {
            heartBeat.Stop();
        }
    }

    void HandleCheckInWarning(int checkInType)
    {
        if (checkInType == 0) // Grace period
        {
            StartCoroutine(GracePeriod());
        }
    }

    private IEnumerator GracePeriod()
    {
        isGracePeriodActive = true;

        if (triggerTracker.triggersEnteredCount == 0) // Play heartbeat if player is not in trigger
        {
            heartBeat.Play();
        }

        yield return new WaitForSeconds(10f); // Grace period duration

        if (isGracePeriodActive)
        {
            isGracePeriodActive = false;
            heartBeat.Stop();


            orangeCheckIn.Play("MildChefCheckIn", 0, 0.0f);
            door.Play();
            isCheckInPlaying = true;
            isCheckInComplete = false;

            yield return new WaitForSeconds(7.5f);
            door.Play();
        }
    }

    private void TriggerJumpscare()
    {
        if (!jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Jumpscare"))
        {
            jumpscare.Play("Jumpscare");
            door.enabled = false;
            pauseScript.enabled = false;
            pmbScript.enabled = false;
            heartBeat.Stop();
            jumpScareBackground.SetActive(true);
            jumpScareAudio.Play();
            pcScript.enabled = false;
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
            orangeCheckIn.ResetTrigger("MildChefCheckIn");
            isCheckInPlaying = false;
            isCheckInComplete = false;

            scannerScript.orangeLight.SetActive(false);
        }
    }

}
