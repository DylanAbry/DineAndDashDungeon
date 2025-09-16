using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusButtonHandler : MonoBehaviour
{
    public PlayerInteraction piScript;
    public PlayerMovement pmScript;
    public PlayerCam pcScript;
    public ScannerWarningManager scannerScript;
    public ChefCheckIns cciScript;
    public MildCheckIn midScript;
    public Timer timerScript;

    public GameObject transitionImage;

    public PanelManagementBeginning pmbScript;
    public Button yesButton;
    public Button noButton;

    [SerializeField] public Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (pmbScript.GetComponent<PanelManagementBeginning>().bonusInstructions[1].activeInHierarchy)
        {
            
            scannerScript.enabled = false;
            pcScript.enabled = false;
            scannerScript.CancelInvoke("ScannerWarning");
            cciScript.enabled = false;
            pmScript.enabled = false;
            piScript.enabled = false;
            midScript.enabled = false;
            timerScript.enabled = false;
            transitionImage.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DisableKeyboardInput();
        }
    }

    public void OnYesButtonClicked()
    {

        yesButton.enabled = false;
        noButton.enabled = false;
        transitionImage.SetActive(true);
        pmbScript.GetComponent<PanelManagementBeginning>().bonusInstructions[1].SetActive(false);
        pmbScript.GetComponent<PanelManagementBeginning>().bonusInstructions[2].SetActive(true);

        pmbScript.GetComponent<PanelManagementBeginning>().currentBonusPanel++;
    }

    public void OnNoButtonClicked()
    {
        
        transitionImage.SetActive(true);
        StartCoroutine(WaitToLoadEscapeScene());
    }

    private IEnumerator WaitToLoadEscapeScene()
    {
        yield return new WaitForSeconds(1f);
        animator.Play("FadeIn"); // Play the fade-in animation
        SceneManager.LoadSceneAsync("DDDEscapeAnimationScene");
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
