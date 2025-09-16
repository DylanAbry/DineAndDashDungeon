using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;

    public GameObject holder;

    public GameObject holderParent;

    public UnityEvent onInteraction;
    // Start is called before the first frame update
    void Start()
    {
        holder.transform.SetParent(holderParent.transform, false);
        holder.SetActive(false);
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {

        onInteraction.Invoke();
        holder.SetActive(true);
        
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
