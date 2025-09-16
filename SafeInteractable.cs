using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeInteractable : MonoBehaviour
{
    public UnityEvent onInteraction;
    public AudioSource safeDoor;

    [SerializeField] public Animator safeOpen = null;
    [SerializeField] public Animator safeClose = null;

    public bool safeOpened;
    public bool firstSafeOpen;

    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        safeOpened = false;
        firstSafeOpen = false;
    }


    public void OpenSafe()
    {
        
        onInteraction.Invoke();
        safeDoor.Play();
        safeOpen.Play("SafeOpen", 0, 0.0f);
        safeOpened = true;
        firstSafeOpen = true;   
    }

    public void CloseSafe()
    {
        onInteraction.Invoke();
        safeDoor.Play();
        safeClose.Play("SafeClose", 0, 0.0f);
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
