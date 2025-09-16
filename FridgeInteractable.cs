using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FridgeInteractable : MonoBehaviour
{
    [SerializeField] public Animator fridgeAnimator = null;
   

    public AudioSource fridgeDoor;

    public bool fridgeInteracted = false;

    public UnityEvent onInteraction;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }
    private void Update()
    {
        if (fridgeInteracted)
        {
            // Use GetCurrentAnimatorStateInfo to check if the animation is still playing
            AnimatorStateInfo stateInfo = fridgeAnimator.GetCurrentAnimatorStateInfo(0);

            // Check if the animation has completed by checking normalizedTime
            if (stateInfo.normalizedTime >= 1.0f && (stateInfo.IsName("FridgeOpen") || stateInfo.IsName("FridgeClose")))
            {
                // If animation is done, set fridgeInteracted to false
                fridgeInteracted = false;
            }
        }
    }

    public void OpenFridge()
    {
        fridgeInteracted = true;
        onInteraction.Invoke();
        fridgeAnimator.Play("FridgeOpen", 0, 0.0f);
        fridgeDoor.Play();
    }

    public void CloseFridge()
    {
        fridgeInteracted = true;
        onInteraction.Invoke();
        fridgeAnimator.Play("FridgeClose", 0, 0.0f);
        fridgeDoor.Play();
        
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
