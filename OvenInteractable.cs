using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OvenInteractable : MonoBehaviour
{
    [SerializeField] public Animator ovenAnimator = null;

    public UnityEvent onInteraction;
    public AudioSource ovenDoor;

    public bool ovenInteracted = false;

    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    private void Update()
    {
        if (ovenInteracted)
        {
            // Use GetCurrentAnimatorStateInfo to check if the animation is still playing
            AnimatorStateInfo stateInfo = ovenAnimator.GetCurrentAnimatorStateInfo(0);

            // Check if the animation has completed by checking normalizedTime
            if (stateInfo.normalizedTime >= 1.0f && (stateInfo.IsName("OvenOpen") || stateInfo.IsName("OvenClose")))
            {
                // If animation is done, set fridgeInteracted to false
                ovenInteracted = false;
            }
        }
    }


    public void OpenOven()
    {
        ovenInteracted = true;
        onInteraction.Invoke();
        ovenAnimator.Play("OvenOpen", 0, 0.0f);
        ovenDoor.Play();
        
    }
    public void CloseOven()
    {
        ovenInteracted = true;
        onInteraction.Invoke();
        ovenAnimator.Play("OvenClose", 0, 0.0f);
        ovenDoor.Play();
        
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
