using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ComputerInteractable : MonoBehaviour
{
    Outline outline;
    public UnityEvent onInteraction;

    public GameObject passWordInput;
    public GameObject cipherInfo;

    public ReadInput inputField;

    public PlayerInteraction piScript;
    public PlayerMovement pmScript;
    public PlayerCam pcScript;


    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        passWordInput.SetActive(false);
        cipherInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (passWordInput.activeInHierarchy)
        {
            inputField.GetComponent<ReadInput>().inputField.ActivateInputField();
            piScript.enabled = false;
            pmScript.enabled = false;
            pcScript.enabled = false;
            if (Input.GetButtonDown("SpaceBar"))
            {
                inputField.GetComponent<ReadInput>().inputField.DeactivateInputField();
                passWordInput.SetActive(false);
                cipherInfo.SetActive(false);
                piScript.enabled = true;
                pmScript.enabled = true;
                pcScript.enabled = true;
            }
        }
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    public void Interact()
    {
        onInteraction.Invoke();
        passWordInput.SetActive(true);
        cipherInfo.SetActive(true);

    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
