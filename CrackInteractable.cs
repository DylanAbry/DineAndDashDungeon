using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class CrackInteractable : MonoBehaviour
{
    public GameObject needPanel;
    public GameObject finalTNT;
    public GameObject spark;
    public GameObject transitionImage;
    public UnityEvent onInteraction;
    public AudioSource countdown;
    public AudioSource explosion;
    public AudioSource wrongBuzzer;
    public PlayerInteraction piScript;
    public MatchInteractable matchScript;
    public PauseGame pauseScript;
    public ScannerWarningManager scannerScript;
    public ChefCheckIns cciScript;
    public MildCheckIn midScript;
    public Timer timerScript;
    public GameObject tntIcon;
    public bool isInteractable;
    [SerializeField] TextMeshProUGUI matchesNeeded;
    [SerializeField] TextMeshProUGUI tntNeeded;
    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        pauseScript.enabled = true;
        outline = GetComponent<Outline>();
        DisableOutline();
        tntNeeded.enabled = false;
        matchesNeeded.enabled = false;
        needPanel.SetActive(false);
        finalTNT.SetActive(false);
        spark.SetActive(false);
        isInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {
        tntNeeded.text = string.Format("TNT Barrels Needed: " + (12 - piScript.numTNT));

        if (finalTNT.activeInHierarchy && spark.activeInHierarchy)
        {
            DisableOutline();
        }

        if (!isInteractable)
        {
            DisableOutline();
        }
    }

    public void Interact()
    {
        if (!isInteractable) return;

        if ((piScript.GetComponent<PlayerInteraction>().numTNT == 12) && (matchScript.GetComponent<MatchInteractable>().matchHolder.activeInHierarchy))
        {
            cciScript.CancelCheckIn();
            midScript.CancelCheckIn();
            finalTNT.SetActive(true);
            spark.SetActive(true);
            matchScript.matchHolder.SetActive(false);
            tntIcon.SetActive(false);
            countdown.Play();
            scannerScript.CancelInvoke("ScannerWarning");
            scannerScript.enabled = false;
            cciScript.enabled = false;
            timerScript.enabled = false;
            midScript.enabled = false;
            needPanel.SetActive(false);
            matchesNeeded.enabled = false;
            pauseScript.enabled = false;
            tntNeeded.enabled = false;
            isInteractable = false;
            StartCoroutine(FinalEscape());
            
        }
        else if ((piScript.GetComponent<PlayerInteraction>().numTNT == 12) && !(matchScript.GetComponent<MatchInteractable>().matchHolder.activeInHierarchy))
        {
            wrongBuzzer.Play();
            needPanel.SetActive(true);
            matchesNeeded.enabled = true;
            StartCoroutine(RemoveNeedPanel());
        }
        else if ((piScript.GetComponent<PlayerInteraction>().numTNT != 12) && (matchScript.GetComponent<MatchInteractable>().matchHolder.activeInHierarchy))
        {
            wrongBuzzer.Play();
            needPanel.SetActive(true);
            tntNeeded.enabled = true;
            StartCoroutine(RemoveNeedPanel());
        }
        else
        {
            wrongBuzzer.Play();
            needPanel.SetActive(true);
            matchesNeeded.enabled = true;
            tntNeeded.enabled = true;
            StartCoroutine(RemoveNeedPanel());
        }
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

    private IEnumerator RemoveNeedPanel()
    {
        yield return new WaitForSeconds(4f);
        needPanel.SetActive(false);
        matchesNeeded.enabled = false;
        tntNeeded.enabled = false;
    }

    private IEnumerator FinalEscape()
    {
        yield return new WaitForSeconds(2.8f);
        if (timerScript.GetComponent<Timer>().bonusHorrorTheme.isPlaying)
        {
            timerScript.bonusHorrorTheme.Stop();
        }
        timerScript.enabled = false;
        countdown.Stop();
        explosion.Play();
        transitionImage.SetActive(true);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadSceneAsync("CasinoEndingScene");
    }

}
