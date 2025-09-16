using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScannerWarningManager : MonoBehaviour
{
    public static ScannerWarningManager Instance; // Singleton for easy access

    public AudioSource alarm;
    public GameObject orangeLight;
    public GameObject redLight;
    public GameObject checkInInstructions;
    public float timeBetween;
    public float bonusTimeBetween;
    private bool firstCheckInOccurred = false;

    public PlayerInteraction piScript;
    public PlayerMovement pmScript;

    public delegate void CheckInEventHandler(int checkInType); // Event handler
    public event CheckInEventHandler OnCheckInWarning;

    void Awake()
    {
        // Singleton pattern to ensure there's only one instance of this manager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        orangeLight.SetActive(false);
        redLight.SetActive(false);
        timeBetween = Random.Range(60f, 90f);
        bonusTimeBetween = Random.Range(40f, 60f);
        InvokeRepeating("ScannerWarning", 5f, timeBetween);
        checkInInstructions.SetActive(false);


        piScript.enabled = true;
        pmScript.enabled = true;
        
    }

    void Update()
    {
        if (checkInInstructions.activeInHierarchy)
        {
            // Check if the game is already paused to avoid unnecessary freeze
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f; // Pauses the game
                if (alarm.isPlaying)
                {
                    alarm.Stop();
                }
            }

            // Use unscaled time input detection for handling user input when paused
            if (Input.GetButtonDown("SpaceBar"))
            {
                checkInInstructions.SetActive(false);
                Time.timeScale = 1f;
                StartCoroutine(Alarm());
                
            }
        }

    }

    void ScannerWarning()
    {

        if (!firstCheckInOccurred)
        {
            firstCheckInOccurred = true; // Mark that the first check-in has occurred
            checkInInstructions.SetActive(true); // Show instructions for the first check-in
        }

        int checkInType = Random.Range(0, 2);

        // Activate lights based on check-in type
        if (checkInType == 0)
        {
            orangeLight.SetActive(true);
            StartCoroutine(Alarm());
            StartCoroutine(OrangeScannerSpan(orangeLight));
        }
        else
        {
            redLight.SetActive(true);
            StartCoroutine(Alarm());
            StartCoroutine(RedScannerSpan(redLight));
        }

        // Invoke the event to notify subscribers (ChefCheckIn, MildCheckIn)
        if (OnCheckInWarning != null)
        {
            OnCheckInWarning.Invoke(checkInType);
        }

        // Reset the time interval
        timeBetween = Random.Range(60f, 90f);
    }

    private IEnumerator RedScannerSpan(GameObject light)
    {
        yield return new WaitForSeconds(20f);
        light.SetActive(false);
    }

    private IEnumerator OrangeScannerSpan(GameObject light)
    {
        yield return new WaitForSeconds(18f);
        light.SetActive(false);
    }

    private IEnumerator Alarm()
    {
        alarm.Play();
        yield return new WaitForSeconds(1.8f);
        alarm.Stop();
    }
}
