using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteInteractable : MonoBehaviour
{
    public GameObject note;
    public GameObject noteImage;

    public PlayerMovement pmScript;
    public MoveCamera mcScript;
    public PlayerInteraction piScript;
    public PlayerCam pcScript;

    public UnityEvent onInteraction;

    public bool noteRead;

    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        noteImage.SetActive(false);
        outline = GetComponent<Outline>();
        DisableOutline();
        noteRead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (noteImage.activeInHierarchy)
        {
            pmScript.canMove = false;
            piScript.enabled = false;
            pcScript.enabled = false;
            mcScript.enabled = false;
            if (Input.GetButtonDown("SpaceBar"))
            {
                noteImage.SetActive(false);
                piScript.enabled = true;
                pmScript.canMove = true;
                pcScript.enabled = true;
                mcScript.enabled = true;
                noteRead = true;
            }
        }
    }

    public void Interact()
    {
        onInteraction.Invoke();
        noteImage.SetActive(true);
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
