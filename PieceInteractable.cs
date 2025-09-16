using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceInteractable : MonoBehaviour
{
    public UnityEvent onInteraction;

    public GameObject holderParent;
    public GameObject holder;

    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        holder.transform.SetParent(holderParent.transform, false);
        holder.SetActive(false);
    }

    public void Interact()
    {
        onInteraction.Invoke();
        holder.SetActive(true);
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
