using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] public Animator openDoor = null;
    [SerializeField] public Animator closeDoor = null;
    Outline outline;

    public AudioSource correct;
    public AudioSource door;
    public bool firstDoorOpen;

    public UnityEvent onInteraction;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        firstDoorOpen = true;
    }

    

    public void OpenInteract()
    {
        if (firstDoorOpen == true)
        {
            firstDoorOpen = false;
            StartCoroutine(PlayCorrectSound());
        }
        
        onInteraction.Invoke();
        door.Play();
        openDoor.Play("DoorOpen", 0, 0.0f);
        
    }
    
    public void CloseInteract()
    {
        onInteraction.Invoke();
        door.Play();
        closeDoor.Play("DoorClose", 0, 0.0f);
        
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

    private IEnumerator PlayCorrectSound()
    {
        if (!correct.isPlaying)
        {
            correct.Play();
        }

        yield return new WaitForSeconds(1f);

        if (correct.isPlaying)
        {
            correct.Stop();
        }
    }
}
