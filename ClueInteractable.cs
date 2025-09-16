using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClueInteractable : MonoBehaviour
{
    public GameObject clue;
    public GameObject clueImage;

    public PlayerMovement pmScript;
    public PlayerInteraction piScript;
    public PlayerCam pcScript;
    public MoveCamera mcScript;

    public UnityEvent onInteraction;

    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        clueImage.SetActive(false);
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (clueImage.activeInHierarchy)
        {
            pmScript.canMove = false;
            piScript.enabled = false;
            pcScript.enabled = false;
            mcScript.enabled = false;

            if (Input.GetButtonDown("SpaceBar")) 
            {
                clueImage.SetActive(false);
                piScript.enabled = true;
                pmScript.canMove = true;
                pcScript.enabled = true;
                mcScript.enabled = true;
            }
        }
    }

    public void Interact()
    {
        onInteraction.Invoke();
        clueImage.SetActive(true);
        
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
