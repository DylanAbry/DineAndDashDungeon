using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyInteractable : MonoBehaviour
{
    public UnityEvent onInteraction;

    public GameObject pieceOneHolder;
    public GameObject pieceTwoHolder;
    public GameObject pieceThreeHolder;

    public GameObject cipherKey;

    public GameObject fragOne;
    public GameObject fragTwo;
    public GameObject fragThree;

    public PuzzleComplete puzzScript;

    private bool onePlaced;
    private bool twoPlaced;
    private bool threePlaced;

    Outline outline;

    public PlayerInteraction piScript;
    public PlayerCam pcScript;
    public PlayerMovement pmScript;

    // Start is called before the first frame update
    void Start()
    {
        fragOne.SetActive(false);
        fragTwo.SetActive(false);
        fragThree.SetActive(false);

        pcScript.enabled = true;
        pmScript.enabled = true;
        piScript.enabled = true;

        onePlaced = false;
        twoPlaced = false;
        threePlaced = false;

        cipherKey.SetActive(false);

        outline = GetComponent<Outline>();
        DisableOutline();
    }

    private void Update()
    {
        if (onePlaced)
        {
            fragOne.SetActive(true);
        }
        if (twoPlaced)
        {
            fragTwo.SetActive(true);
        }
        if (threePlaced)
        {
            fragThree.SetActive(true);
        }

        if (cipherKey.activeInHierarchy)
        {
            piScript.enabled = false;
            pmScript.enabled = false;
            pcScript.enabled = false;
            if (Input.GetButtonDown("SpaceBar"))
            {
                cipherKey.SetActive(false);
                piScript.enabled = true;
                pmScript.enabled = true;
                pcScript.enabled = true;
            }
        }
    }

    public void Interact()
    {
         if (pieceOneHolder.activeInHierarchy)
         {
             onInteraction.Invoke();
             fragOne.SetActive(true);
             pieceOneHolder.SetActive(false);
             onePlaced = true;
         }
         if (pieceTwoHolder.activeInHierarchy)
         {
             onInteraction.Invoke();
             fragTwo.SetActive(true);
             pieceTwoHolder.SetActive(false);
             twoPlaced = true;
         }
         if (pieceThreeHolder.activeInHierarchy)
         {
             onInteraction.Invoke();
             fragThree.SetActive(true);
             pieceThreeHolder.SetActive(false);
             threePlaced = true;
         }
         if (puzzScript.GetComponent<PuzzleComplete>().keyPuzzleComplete())
        {
            onInteraction.Invoke();
            cipherKey.SetActive(true);
        }
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
