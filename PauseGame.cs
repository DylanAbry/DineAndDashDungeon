using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    public PuzzleComplete puzzScript;
    public PanelManagementBeginning pmbScript;


    public GameObject pausePanel;
    public GameObject controlPanel;
    public GameObject challengePanel;
    public GameObject quitMenu;

    public GameObject challengeOne;
    public GameObject challengeTwo;
    public GameObject challengeThree;

    private GameObject currentChallenge;

    public Button backToGame;
    public Button controls;
    public Button challenge;
    public Button exitToMenu;

    public Button backButton;

    public Button yesQuit;
    public Button noQuit;

    public ScannerWarningManager scannerScript;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        controlPanel.SetActive(false);
        challengePanel.SetActive(false);
        quitMenu.SetActive(false);

        backToGame.onClick.AddListener(ResumeGame);
        challenge.onClick.AddListener(DisplayCurrentChallenge);
        controls.onClick.AddListener(DisplayControls);
        exitToMenu.onClick.AddListener(DisplayQuitMenu);

        backButton.onClick.AddListener(ReturnToPauseMenu);
        yesQuit.onClick.AddListener(Quit);
        noQuit.onClick.AddListener(ReturnToPauseMenu);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentChallenge = challengeOne;
        currentChallenge.SetActive(false);
        challengeTwo.SetActive(false);
        challengeThree.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isPaused)
        {
            Pause();
        }

        if (isPaused)
        {
            DisableKeyboardInput();
        }

        if (puzzScript.cipherPuzzleComplete())
        {
            currentChallenge = challengeTwo;
        }

        if (pmbScript.bonusChallengeAccepted == true)
        {
            currentChallenge = challengeThree;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unfreeze game time
        pausePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freezes game time
        pausePanel.SetActive(true);

        if (scannerScript.GetComponent<ScannerWarningManager>().alarm.isPlaying)
        {
            scannerScript.GetComponent<ScannerWarningManager>().alarm.Stop();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;



        // Enable only mouse input (disable keyboard)
        DisableKeyboardInput();
    }

    public void DisplayControls()
    {
        pausePanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void DisplayCurrentChallenge()
    {
        challengePanel.SetActive(true);
        currentChallenge.SetActive(true);
    }

    public void DisplayQuitMenu()
    {
        quitMenu.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
        controlPanel.SetActive(false);
        challengePanel.SetActive(false);
        quitMenu.SetActive(false);
        pausePanel.SetActive(true);
        currentChallenge.SetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("DDDTitleScene");
    }

    public void DisableKeyboardInput()
    {
        if (Input.anyKeyDown)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
        }
    }
}
