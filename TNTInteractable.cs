using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TNTInteractable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tntCountDisplay;
    public UnityEvent onInteraction;
    public PlayerInteraction piScript;
    public GameObject tntIcon;
    public bool isInteractable;

    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        tntIcon.SetActive(false);
        outline = GetComponent<Outline>();
        isInteractable = false;
        DisableOutline();
    }
    private void Update()
    {     
        if (!isInteractable)
        {
            DisableOutline();
        }
    }

    public void Interact()
    {

        if (!isInteractable) return;

        if (piScript.GetComponent<PlayerInteraction>().numTNT == 0)
        {
            tntIcon.SetActive(true);
        }

        onInteraction.Invoke();
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
