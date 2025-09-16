using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManagementBeginning : MonoBehaviour
{
    public AudioSource safeSparkle;
    public AudioSource mainTheme;
    public GameObject wallNotif;
    public GameObject cipherInstructions;
    public GameObject recollectionInstructions;
    public GameObject[] bonusInstructions;
    public GameObject wall;
    public GameObject lifeInsurance;
    public PlayerMovement pmScript;
    public MoveCamera mcScript;
    public PlayerInteraction piScript;
    public Timer timerScript;
    public PuzzleComplete puzzScript;
    public ChefCheckIns cciScript;
    public PlayerCam pcScript;
    public MildCheckIn midScript;
    public ScannerWarningManager scannerScript;
    public SafeInteractable safeScript;
    public TNTInteractable[] tntScript;
    public MatchInteractable matchScript;
    public CrackInteractable crackScript;
    public PauseGame pauseScript;


    public int currentBonusPanel = 0;
    public bool messageDisplayed;
    public bool bonusChallengeAccepted;

    void Start()
    {
        
        tntScript = FindObjectsOfType<TNTInteractable>();

        // Initially hide the panels
        messageDisplayed = false;
        cipherInstructions.SetActive(false);
        recollectionInstructions.SetActive(false);

        for (int i = 0; i < bonusInstructions.Length; i++)
        {
            bonusInstructions[i].SetActive(false);
        }

        // Show cipher instructions after 2 seconds from the game start
        Invoke("CipherPuzzleInstructions", 2);


        wallNotif.SetActive(false);
        bonusChallengeAccepted = false;

        foreach (TNTInteractable script in tntScript)
        {
            script.isInteractable = false;
        }

        lifeInsurance.SetActive(true);

    }

    void Update()
    {
        // Disable player movement and interaction while any instructions are active
        if (cipherInstructions.activeInHierarchy)
        {
            Time.timeScale = 0f;  // Pause the game

            // Detect spacebar press using KeyCode.Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;  // Resume the game
                cipherInstructions.SetActive(false);

                // Re-enable player scripts and other components
                piScript.enabled = true;
                pmScript.enabled = true;
                scannerScript.enabled = true;
                midScript.enabled = true;
                cciScript.enabled = true;
            }
        }

        if (recollectionInstructions.activeInHierarchy)
        {
            Time.timeScale = 0f;  // Pause the game

            // Detect spacebar press using KeyCode.Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;  // Resume the game
                recollectionInstructions.SetActive(false);

                // Re-enable player scripts and other components
                piScript.enabled = true;
                pmScript.enabled = true;
                scannerScript.enabled = true;
                midScript.enabled = true;
                cciScript.enabled = true;
            }
        }

        if (puzzScript.GetComponent<PuzzleComplete>().cipherPuzzleComplete())
        {
            if (!messageDisplayed)
            {
                wallNotif.SetActive(true);
            }
            else
            {
                wallNotif.SetActive(false);
            }
        }
        if (safeScript.GetComponent<SafeInteractable>().firstSafeOpen == true)
        {

            StartCoroutine(LoadNextStepPanels());

            if (Input.GetButtonDown("SpaceBar"))
            {

                // Ensure currentBonusPanel is within the bounds of the array
                if (currentBonusPanel < bonusInstructions.Length)
                {
                    bonusInstructions[currentBonusPanel].SetActive(false);
                    currentBonusPanel++;

                    // If currentBonusPanel exceeds the array bounds, cap it
                    if (currentBonusPanel >= bonusInstructions.Length)
                    {
                        currentBonusPanel = bonusInstructions.Length - 1;  // Ensure it stays within the array bounds
                        return;
                    }
                }
            }

            if (bonusInstructions[2].activeInHierarchy)
            {
                bonusInstructions[1].SetActive(false);
                bonusChallengeAccepted = true;
                cciScript.enabled = false;
                mcScript.enabled = false;
                pmScript.canMove = false;
                piScript.enabled = false;
                midScript.enabled = false;
                pcScript.enabled = false;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (Input.GetButtonDown("SpaceBar"))
                {
                    bonusInstructions[2].SetActive(false);
                  
                    cciScript.enabled = true;
                    mcScript.enabled = true;
                    pmScript.canMove = true;
                    pauseScript.enabled = true;
                    piScript.enabled = true;
                    midScript.enabled = true;
                    pcScript.enabled = true;
                    timerScript.enabled = true;
                    scannerScript.enabled = true;
                    lifeInsurance.SetActive(false);
                    scannerScript.enabled = true;
                    scannerScript.InvokeRepeating("ScannerWarning", 5f, scannerScript.timeBetween);

                    if (safeSparkle.isPlaying)
                    {
                        safeSparkle.Stop();
                    }

                    foreach (TNTInteractable script in tntScript)
                    {
                        script.isInteractable = true;  // Now TNT can be interacted with   
                    }

                    matchScript.isInteractable = true;
                    crackScript.isInteractable = true;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Show recollection instructions only once when player enters the trigger
        if (other.CompareTag("Player") && !messageDisplayed)
        {
            
            StartCoroutine(WaitForRecollectionInstructions());
            messageDisplayed = true;
            wall.GetComponent<Outline>().enabled = false; // Assuming this disables an outline effect
            
        }
    }

    // Show cipher instructions after 2 seconds at the start
    private void CipherPuzzleInstructions()
    {
        cipherInstructions.SetActive(true);
    }

    // Wait 2 seconds after trigger to show recollection instructions
    private IEnumerator WaitForRecollectionInstructions()
    {
        yield return new WaitForSeconds(2f);
        recollectionInstructions.SetActive(true);
    }
 

    private IEnumerator LoadNextStepPanels()
    {
        if (mainTheme.isPlaying)
        {
            mainTheme.Stop();
        }

        if (currentBonusPanel < bonusInstructions.Length)
        {
            midScript.CancelCheckIn();
            cciScript.CancelCheckIn();
            scannerScript.CancelInvoke("ScannerWarning");
            cciScript.enabled = false;
            midScript.enabled = false;
            pauseScript.enabled = false;
            if (pauseScript.pausePanel.activeInHierarchy || pauseScript.controlPanel.activeInHierarchy || pauseScript.challengePanel.activeInHierarchy || pauseScript.quitMenu.activeInHierarchy)
            {
                pauseScript.pausePanel.SetActive(false);
                pauseScript.controlPanel.SetActive(false);
                pauseScript.quitMenu.SetActive(false);
                pauseScript.challengePanel.SetActive(false);
            }
            scannerScript.enabled = false;
            timerScript.enabled = false;
            pcScript.enabled = false;
            pmScript.canMove = false;
            piScript.enabled = false;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(MoneyTheme());
        if (currentBonusPanel < bonusInstructions.Length)
        {
            bonusInstructions[currentBonusPanel].SetActive(true);
        }
    }

    private IEnumerator MoneyTheme()
    {

        if (!safeSparkle.isPlaying)
        {
            safeSparkle.Play();
        }

        // Wait for 7 seconds before stopping the audio
        yield return new WaitForSeconds(7f);

        // Gradually lower the volume to make it smoother
        float fadeDuration = 0.5f;  // Duration for fading out
        float startVolume = safeSparkle.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            safeSparkle.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        safeSparkle.Stop();  // Stop after fade-out
        safeSparkle.volume = startVolume;  // R
    }

}
