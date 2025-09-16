using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] public Animator jumpscare = null;
    [SerializeField] public Animator fadeIn = null;
    public AudioSource jumpScareAudio;
    public AudioSource bonusHorrorTheme;
    public GameObject backgroundJumpscare;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public PanelManagementBeginning pmbScript;
    private bool bonusChallengeStarted;
    private bool jumpScareTriggered;

    // Update is called once per frame
    void Update()
    {

        if (pmbScript.bonusChallengeAccepted && !bonusChallengeStarted)
        {
            StartBonusChallenge();
            bonusChallengeStarted = true;  // Ensures this block runs only once
        }

        if (pmbScript.bonusInstructions[0].activeInHierarchy || pmbScript.bonusInstructions[1].activeInHierarchy)
        {
            timerText.enabled = false;
        }
        if (pmbScript.bonusInstructions[2].activeInHierarchy)
        {
            timerText.enabled = true;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Ensure the time counts down after being set to 300 seconds
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (remainingTime <= 0f && !jumpScareTriggered)
        {
            TriggerJumpscare();
        }
    }

    void StartBonusChallenge()
    {
        remainingTime = 300f;  // Set the remaining time to 300 seconds
        bonusHorrorTheme.Play();  // Start the bonus horror theme
    }

    private void TriggerJumpscare()
    {
        remainingTime = 0;
        timerText.enabled = false;
        pmbScript.enabled = false;
        jumpscare.Play("Jumpscare", 0, 0f);
        backgroundJumpscare.SetActive(true);
        jumpScareAudio.Play();
        jumpScareTriggered = true;
        if (bonusHorrorTheme.isPlaying)
        {
            bonusHorrorTheme.Stop();
        }
        StartCoroutine(WaitForJumpscareToEnd());
    }

    private IEnumerator WaitForJumpscareToEnd()
    {
        // Wait until the jumpscare animation is in the correct state
        while (jumpscare.GetCurrentAnimatorStateInfo(0).IsName("Jumpscare") == false)
        {
            yield return null;  // Wait for the next frame
        }

        // Now, wait for the jumpscare animation to finish
        yield return new WaitForSeconds(jumpscare.GetCurrentAnimatorStateInfo(0).length + 3f);

        // Fade out and load scene
        if (fadeIn != null)
        {
            fadeIn.Play("FadeIn");
            yield return new WaitForSeconds(fadeIn.GetCurrentAnimatorStateInfo(0).length);
        }

        SceneManager.LoadScene("DeathEndingScene");
    }
}
