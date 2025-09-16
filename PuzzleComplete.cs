using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleComplete : MonoBehaviour
{
    public DoorInteractable door;

    public ComputerInteractable ciScript;
    public PlayerInteraction piScript;

    public GameObject wall;

    public AudioSource correct;
    public GameObject burger;
    public GameObject whiskey;
    public GameObject stew;
    public GameObject pizza;
    public GameObject steak;
    public GameObject hotdog;
    public GameObject pancake;

   

    public GameObject fragOne;
    public GameObject fragTwo;
    public GameObject fragThree;


    // Start is called before the first frame update
    void Start()
    {
        door.GetComponent<DoorInteractable>().openDoor.enabled = false;
        door.GetComponent<DoorInteractable>().closeDoor.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPuzzleComplete())
        {
            ciScript.enabled = true;
        }
        if (!keyPuzzleComplete())
        {
            ciScript.enabled = false;
        }
        if (cipherPuzzleComplete())
        {
            
            ciScript.GetComponent<ComputerInteractable>().passWordInput.SetActive(false);
            ciScript.GetComponent<ComputerInteractable>().cipherInfo.SetActive(false);
            ciScript.GetComponent<ComputerInteractable>().pcScript.enabled = true; ;
            ciScript.GetComponent<ComputerInteractable>().piScript.enabled = true; ;
            ciScript.GetComponent<ComputerInteractable>().pmScript.enabled = true;
            ciScript.enabled = false;
        }
        if (logicPuzzleCompleted())
        {
            door.GetComponent<DoorInteractable>().openDoor.enabled = true;
            door.GetComponent<DoorInteractable>().closeDoor.enabled = true;
        }
    }
    public bool keyPuzzleComplete()
    {
        if (fragOne.activeInHierarchy && fragTwo.activeInHierarchy && fragThree.activeInHierarchy)
        {
            return true;
        }

        return false;
    }
    

    public bool cipherPuzzleComplete()
    {
        if (wall.GetComponent<Collider>().isTrigger == true)
        {
            
            return true;
        }
        return false;
    }

    public bool logicPuzzleCompleted()
    {
        if (burger.activeInHierarchy && whiskey.activeInHierarchy && hotdog.activeInHierarchy && pizza.activeInHierarchy
            && steak.activeInHierarchy && stew.activeInHierarchy && pancake.activeInHierarchy)
        {
            return true;
        }

        return false;
    }

}
