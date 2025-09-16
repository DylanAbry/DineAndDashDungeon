using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatchInteractable : MonoBehaviour
{
    public UnityEvent onInteraction;
    public bool isInteractable;
    Outline outline;

    public GameObject matchHolder;

    // Start is called before the first frame update
    void Start()
    {
        matchHolder.SetActive(false);
        outline = GetComponent<Outline>();
        isInteractable = false;
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

        onInteraction.Invoke();
        matchHolder.SetActive(true);
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
