using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    public int numTNT;

    public AudioSource wrongBuzzer;

    [SerializeField] TextMeshProUGUI tntCountDisplay;

    public GameObject finalTNT;
    public GameObject sparks;

    public PuzzleComplete puzzScript;
    public NoteInteractable noteScript;
    public SafeInteractable safeScript;
    public DoorInteractable doorScript;
    

    public GameObject burger;
    public GameObject[] burgerChildren;
    public GameObject burgerHolder;

    public GameObject whiskey;
    public GameObject[] whiskeyChildren;
    public GameObject whiskeyHolder;

    public GameObject hotdog;
    public GameObject[] hotdogChildren;
    public GameObject hotdogHolder;

    public GameObject pizza;
    public GameObject[] pizzaChildren;
    public GameObject pizzaHolder;

    public GameObject steak;
    public GameObject[] steakChildren;
    public GameObject steakHolder;

    public GameObject stew;
    public GameObject[] stewChildren;
    public GameObject stewHolder;

    public GameObject pancake;
    public GameObject[] pancakeChildren;
    public GameObject pancakeHolder;

    public GameObject tnt;
    public GameObject[] tntChildren;
    public GameObject tntIcon;


    public GameObject fragOne;
    public GameObject fragTwo;
    public GameObject fragThree;


    public GameObject pieceOneHolder;
    public GameObject pieceTwoHolder;
    public GameObject pieceThreeHolder;

    

    public GameObject noEntryMessage;

    private bool placed;
    public bool doorClosed;
    public bool ovenClosed;
    public bool fridgeClosed;
    public bool safeClosed;
    

    public DropObjects burgerDropped;
    public DropObjects whiskeyDropped;
    public DropObjects hotdogDropped;
    public DropObjects steakDropped;
    public DropObjects stewDropped;
    public DropObjects pizzaDropped;
    public DropObjects pancakeDropped;
    

    Interactable currentInteractable;
    TableInteractable currentInteracTable;
    ClueInteractable currentClueInteractable;
    DoorInteractable currentDoorInteractable;
    ComputerInteractable currentCompInteractable;
    PieceInteractable currentPieceInteractable;
    KeyInteractable currentKeyInteractable;
    OvenInteractable currentOvenInteractable;
    FridgeInteractable currentFridgeInteractable;
    TNTInteractable currentTNTInteractable;
    NoteInteractable currentNoteInteractable;
    SafeInteractable currentSafeInteractable;
    MatchInteractable currentMatchInteractable;
    CrackInteractable currentCrackInteractable;


    private void Start()
    {

        numTNT = 0;
        tntCountDisplay.enabled = false;

        noEntryMessage.SetActive(false);

        fragOne.SetActive(false);
        fragTwo.SetActive(false);
        fragThree.SetActive(false);

        // Set every food item, except for the initial items to inactive

        burgerChildren = new GameObject[burger.transform.childCount];
        for (int i = 0; i < burgerChildren.Length; i ++)
        {
            burgerChildren[i] = burger.transform.GetChild(i).gameObject;

            if (i == 0)
            {
                burgerChildren[i].SetActive(true);
            }
            else
            {
                burgerChildren[i].SetActive(false);
            }
        }
        whiskeyChildren = new GameObject[whiskey.transform.childCount];
        for (int i = 0; i < whiskeyChildren.Length; i++)
        {
            whiskeyChildren[i] = whiskey.transform.GetChild(i).gameObject;

            if (i == 0)
            {
                whiskeyChildren[i].SetActive(true);
            }
            else
            {
                whiskeyChildren[i].SetActive(false);
            }
        }

        hotdogChildren = new GameObject[hotdog.transform.childCount];
        for (int i = 0; i < hotdogChildren.Length; i++)
        {
            hotdogChildren[i] = hotdog.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                hotdogChildren[i].SetActive(true);
            }
            else
            {
                hotdogChildren[i].SetActive(false);
            }
        }

        pizzaChildren = new GameObject[pizza.transform.childCount];
        for (int i = 0; i < pizzaChildren.Length; i++)
        {
            pizzaChildren[i] = pizza.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                pizzaChildren[i].SetActive(true);
            }
            else
            {
                pizzaChildren[i].SetActive(false);
            }
        }

        steakChildren = new GameObject[steak.transform.childCount];
        for (int i = 0; i < steakChildren.Length; i++)
        {
            steakChildren[i] = steak.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                steakChildren[i].SetActive(true);
            }
            else
            {
                steakChildren[i].SetActive(false);
            }
        }

        stewChildren = new GameObject[stew.transform.childCount];
        for (int i = 0; i < stewChildren.Length; i++)
        {
            stewChildren[i] = stew.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                stewChildren[i].SetActive(true);
            }
            else
            {
                stewChildren[i].SetActive(false);
            }
        }

        pancakeChildren = new GameObject[pancake.transform.childCount];
        for (int i = 0; i < pancakeChildren.Length; i++)
        {
            pancakeChildren[i] = pancake.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                pancakeChildren[i].SetActive(true);
            }
            else
            {
                pancakeChildren[i].SetActive(false);
            }
        }

        tntChildren = new GameObject[tnt.transform.childCount];
        for (int i = 0; i < tntChildren.Length; i++)
        {
            tntChildren[i] = tnt.transform.GetChild(i).gameObject;
            tntChildren[i].SetActive(true);    
        }

        doorClosed = true;
        placed = true;
        ovenClosed = true;
        fridgeClosed = true;
        safeClosed = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (finalTNT.activeInHierarchy && sparks.activeInHierarchy)
        {
            tntCountDisplay.enabled = false;
        }

        CheckInteraction();
        if (Input.GetMouseButtonDown(0) && currentInteractable != null && (placed == true || burgerDropped.isUsed
            || whiskeyDropped.isUsed || pizzaDropped.isUsed || pancakeDropped.isUsed || steakDropped.isUsed
            || stewDropped.isUsed || hotdogDropped.isUsed) && !(burgerHolder.activeInHierarchy || 
            whiskeyHolder.activeInHierarchy || pizzaHolder.activeInHierarchy || pancakeHolder.activeInHierarchy ||
            hotdogHolder.activeInHierarchy || stewHolder.activeInHierarchy || steakHolder.activeInHierarchy) &&
            doorScript.GetComponent<DoorInteractable>().firstDoorOpen)
        {
            
            currentInteractable.Interact();
            placed = false;
            
        }

        /* This code determines the interactable status of each item (whether or not it is currently being held,
         * as well as whether or not the corresponding table is occupied */

        // Determines the interactable status of burger

        if (burgerHolder.activeInHierarchy)
        {
            
            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                burger.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }
            
        }

        // Determines interactable status of whiskey

        if (whiskeyHolder.activeInHierarchy)
        {
            
            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                whiskey.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }
            
        }

        // Determines interactable status of hotdog

        if (hotdogHolder.activeInHierarchy)
        {

            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                hotdog.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }

        }

        // Determines interactable status of pizza

        if (pizzaHolder.activeInHierarchy)
        {

            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                pizza.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }

        }

        // Determine interactable status of steak

        if (steakHolder.activeInHierarchy)
        {

            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                steak.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }

        }

        // Determine interactable status of stew

        if (stewHolder.activeInHierarchy)
        {

            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                stew.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }

        }

        // Determines interactable status of pancake

        if (pancakeHolder.activeInHierarchy)
        {

            if (Input.GetMouseButtonDown(0) && currentInteracTable != null && currentInteracTable.GetComponent<TableInteractable>().occupied == false)
            {
                currentInteracTable.Interact();
                pancake.SetActive(true);
                placed = true;
                DisableCurrentInteracTable();
            }

        }

        if (Input.GetMouseButtonDown(0) && currentClueInteractable != null)
        {
            currentClueInteractable.Interact();
        }
        if (Input.GetMouseButtonDown(0) && currentDoorInteractable != null)
        {
            if (currentDoorInteractable.GetComponent<PuzzleComplete>().logicPuzzleCompleted() == true)
            {
                if (doorClosed)
                {
                    currentDoorInteractable.OpenInteract();
                    doorClosed = false;
                }
                else
                {
                    currentDoorInteractable.CloseInteract();
                    doorClosed = true;
                }
            }
            else
            {
                noEntryMessage.SetActive(true);
                StartCoroutine(NoDoorEntry());
            }
        }
        
        if (Input.GetMouseButtonDown(0) && currentCompInteractable != null)
        {
            currentCompInteractable.Interact();
        }
        if (Input.GetMouseButtonDown(0) && (currentPieceInteractable != null) && !(pieceOneHolder.activeInHierarchy ||
            pieceTwoHolder.activeInHierarchy || pieceThreeHolder.activeInHierarchy))
        {
            currentPieceInteractable.Interact();
        }

        if (pieceOneHolder.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) && currentKeyInteractable != null)
            {
                currentKeyInteractable.Interact();
                fragOne.SetActive(true);
            }
        }
        if (pieceTwoHolder.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) && currentKeyInteractable != null)
            {
                currentKeyInteractable.Interact();
                fragTwo.SetActive(true);
            }
        }
        if (pieceThreeHolder.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) && currentKeyInteractable != null)
            {
                currentKeyInteractable.Interact();
                fragThree.SetActive(true);
            }
        }
        if (puzzScript.GetComponent<PuzzleComplete>().keyPuzzleComplete())
        {
            if (Input.GetMouseButtonDown(0) && currentKeyInteractable != null)
            {
                currentKeyInteractable.Interact();
            }
        }
        if (Input.GetMouseButtonDown(0) && currentOvenInteractable != null)
        {
            if (ovenClosed)
            {
                currentOvenInteractable.OpenOven();
                ovenClosed = false;
            }
            else
            {
                currentOvenInteractable.CloseOven();
                ovenClosed = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && currentFridgeInteractable != null)
        {
            if (fridgeClosed)
            {
                currentFridgeInteractable.OpenFridge();
                fridgeClosed = false;
            }
            else
            {
                currentFridgeInteractable.CloseFridge();
                fridgeClosed = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && currentNoteInteractable != null)
        {
            currentNoteInteractable.Interact();
        }
        if (noteScript.GetComponent<NoteInteractable>().noteRead == false)
        {
            safeScript.enabled = false;
        }
        if (noteScript.GetComponent<NoteInteractable>().noteRead == true) {

            safeScript.enabled = true;

            if (Input.GetMouseButtonDown(0) && currentSafeInteractable != null)
            {
                if (safeClosed)
                {
                    currentSafeInteractable.OpenSafe();
                    safeClosed = false;
                }
                else
                {
                    currentSafeInteractable.CloseSafe();
                    safeClosed = true;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0) && currentTNTInteractable != null)
        {
            if (currentTNTInteractable.GetComponent<TNTInteractable>().isInteractable)
            {
                if (numTNT == 0)
                {
                    tntCountDisplay.enabled = true;
                }

                currentTNTInteractable.Interact();
                numTNT++;
                tntCountDisplay.text = string.Format(numTNT + "/12");
            }
        }

        if (Input.GetMouseButtonDown(0) && currentMatchInteractable != null)
        {
            if (currentMatchInteractable.GetComponent<MatchInteractable>().isInteractable)
            {
                currentMatchInteractable.Interact();
            }
        }
        if (Input.GetMouseButtonDown(0) && currentCrackInteractable != null)
        {
            if (currentCrackInteractable.GetComponent<CrackInteractable>().isInteractable && !(finalTNT.activeInHierarchy && 
                sparks.activeInHierarchy))
            {
                currentCrackInteractable.Interact();
            }
            if (currentCrackInteractable.GetComponent<CrackInteractable>().isInteractable && (finalTNT.activeInHierarchy &&
                sparks.activeInHierarchy))
            {
                currentCrackInteractable.Interact();
                DisableCurrentCrackInteractable();
            }
        }
        
        
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                

                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // If interactable is not enabled
                {
                    DisableCurrentInteractable();
                }
            }
            else if (hit.collider.tag == "Table")
            {
                TableInteractable newInteracTable = hit.collider.GetComponent<TableInteractable>();


                if (currentInteracTable && newInteracTable != currentInteracTable)
                {
                    currentInteracTable.DisableOutline();
                }
                if (newInteracTable.enabled)
                {
                    SetNewCurrentInteracTable(newInteracTable);
                }
                

                else // If interactable is not enabled
                {
                    DisableCurrentInteractable();
                    DisableCurrentInteracTable();
                }
            }
            else if (hit.collider.tag == "Clue")
            {
                ClueInteractable newClueInteractable = hit.collider.GetComponent<ClueInteractable>();

                if (currentClueInteractable && newClueInteractable != currentClueInteractable)
                {
                    currentClueInteractable.DisableOutline();
                }
                if (newClueInteractable.enabled)
                {
                    SetNewCurrentClueInteractable(newClueInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentClueInteractable();
                }
            }
            else if (hit.collider.tag == "Door")
            {
                DoorInteractable newDoorInteractable = hit.collider.GetComponent<DoorInteractable>();

                if (currentDoorInteractable && newDoorInteractable != currentDoorInteractable)
                {
                    currentDoorInteractable.DisableOutline();
                }
                if (newDoorInteractable.enabled)
                {
                    SetNewCurrentDoorInteractable(newDoorInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentDoorInteractable();
                }
            }
            else if (hit.collider.tag == "Computer")
            {
                ComputerInteractable newCompInteractable = hit.collider.GetComponent<ComputerInteractable>();
                if (currentCompInteractable && newCompInteractable != currentCompInteractable)
                {
                    currentCompInteractable.DisableOutline();
                }
                if (newCompInteractable.enabled)
                {
                    SetNewCurrentCompInteractable(newCompInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentCompInteractable();
                }
            }
            else if (hit.collider.tag == "Piece")
            {
                PieceInteractable newPieceInteractable = hit.collider.GetComponent<PieceInteractable>();
                if (currentPieceInteractable && newPieceInteractable != currentPieceInteractable)
                {
                    currentPieceInteractable.DisableOutline();
                }
                if (newPieceInteractable.enabled)
                {
                    SetNewCurrentPieceInteractable(newPieceInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentPieceInteractable();
                }
            }
            else if (hit.collider.tag == "Key")
            {
                KeyInteractable newKeyInteractable = hit.collider.GetComponent<KeyInteractable>();
                
                if (currentKeyInteractable && newKeyInteractable != currentKeyInteractable)
                {
                    currentKeyInteractable.DisableOutline();
                }
                if (newKeyInteractable.enabled)
                {
                    SetNewCurrentKeyInteractable(newKeyInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentKeyInteractable();
                }
            }
            else if (hit.collider.tag == "Oven")
            {
                OvenInteractable newOvenInteractable = hit.collider.GetComponent<OvenInteractable>();

                if (currentOvenInteractable && newOvenInteractable != currentOvenInteractable)
                {
                    currentOvenInteractable.DisableOutline();
                }
                if (newOvenInteractable.enabled)
                {
                    SetNewCurrentOvenInteractable(newOvenInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentOvenInteractable();
                }
            }
            else if (hit.collider.tag == "Refrigerator")
            {
                FridgeInteractable newFridgeInteractable = hit.collider.GetComponent<FridgeInteractable>();

                if (currentFridgeInteractable && newFridgeInteractable != currentFridgeInteractable)
                {
                    currentFridgeInteractable.DisableOutline();
                }
                if (newFridgeInteractable.enabled)
                {
                    SetNewCurrentFridgeInteractable(newFridgeInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentFridgeInteractable();
                }
            }
            else if (hit.collider.tag == "Note")
            {
                NoteInteractable newNoteInteractable = hit.collider.GetComponent<NoteInteractable>();

                if (currentNoteInteractable && newNoteInteractable != currentNoteInteractable)
                {
                    currentNoteInteractable.DisableOutline();
                }
                if (newNoteInteractable.enabled)
                {
                    SetNewCurrentNoteInteractable(newNoteInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentNoteInteractable();
                }
            }
            else if (hit.collider.tag == "Safe")
            {
                SafeInteractable newSafeInteractable = hit.collider.GetComponent<SafeInteractable>();

                if (currentSafeInteractable && newSafeInteractable != currentSafeInteractable)
                {
                    currentSafeInteractable.DisableOutline();
                }
                if (newSafeInteractable.enabled)
                {
                    SetNewCurrentSafeInteractable(newSafeInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentSafeInteractable();
                }
            }
            else if (hit.collider.tag == "TNT")
            {
                TNTInteractable newTNTInteractable = hit.collider.GetComponent<TNTInteractable>();

                if (currentTNTInteractable && newTNTInteractable != currentTNTInteractable)
                {
                    currentTNTInteractable.DisableOutline();
                }
                if (newTNTInteractable.enabled)
                {
                    SetNewCurrentTNTInteractable(newTNTInteractable);
                }

                else // If interactable is not enabled
                {
                    DisableCurrentTNTInteractable();
                }
            }
            else if (hit.collider.tag == "Match")
            {
                MatchInteractable newMatchInteractable = hit.collider.GetComponent<MatchInteractable>();

                if (currentMatchInteractable && newMatchInteractable != currentMatchInteractable)
                {
                    currentMatchInteractable.DisableOutline();
                }
                if (newMatchInteractable.enabled)
                {
                    SetNewCurrentMatchInteractable(newMatchInteractable);
                }
                else
                {
                    DisableCurrentMatchInteractable();
                }
            }
            else if (hit.collider.tag == "Crack")
            {
                CrackInteractable newCrackInteractable = hit.collider.GetComponent<CrackInteractable>();

                if (currentCrackInteractable && newCrackInteractable != currentCrackInteractable)
                {
                    currentCrackInteractable.DisableOutline();
                }
                if (newCrackInteractable.enabled)
                {
                    SetNewCurrentCrackInteractable(newCrackInteractable);
                }
                else
                {
                    DisableCurrentCrackInteractable();
                }
            }
            else // If object is not interactable
            {
                DisableCurrentInteractable();
                DisableCurrentInteracTable();
                DisableCurrentClueInteractable();
                DisableCurrentDoorInteractable();
                DisableCurrentCompInteractable();
                DisableCurrentPieceInteractable();
                DisableCurrentKeyInteractable();
                DisableCurrentOvenInteractable();
                DisableCurrentFridgeInteractable();
                DisableCurrentTNTInteractable();
                DisableCurrentSafeInteractable();
                DisableCurrentNoteInteractable();
                DisableCurrentMatchInteractable();
                DisableCurrentCrackInteractable();
            }
        }
        else // If nothing is in reach
        {
            DisableCurrentInteractable();
            DisableCurrentInteracTable();
            DisableCurrentClueInteractable();
            DisableCurrentDoorInteractable();
            DisableCurrentCompInteractable();
            DisableCurrentPieceInteractable();
            DisableCurrentKeyInteractable();
            DisableCurrentOvenInteractable();
            DisableCurrentFridgeInteractable();
            DisableCurrentTNTInteractable();
            DisableCurrentSafeInteractable();
            DisableCurrentNoteInteractable();
            DisableCurrentMatchInteractable();
            DisableCurrentCrackInteractable();
        }

    }

    // Enables the outline of interactable objects, unless if a holder is currently active

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        if (doorScript.GetComponent<DoorInteractable>().firstDoorOpen == true)
        {
            if (burgerHolder.activeInHierarchy || whiskeyHolder.activeInHierarchy || steakHolder.activeInHierarchy ||
            stewHolder.activeInHierarchy || pancakeHolder.activeInHierarchy || pizzaHolder.activeInHierarchy ||
            hotdogHolder.activeInHierarchy)
            {
                currentInteractable.DisableOutline();
            }
            else
            {
                currentInteractable.EnableOutline();
            }
        }
        else
        {
            currentInteractable.DisableOutline();
        }
    }

    void SetNewCurrentInteracTable(TableInteractable newInteracTable)
    {
        currentInteracTable = newInteracTable;

        if ((burgerHolder.activeInHierarchy || whiskeyHolder.activeInHierarchy 
            || hotdogHolder.activeInHierarchy || pizzaHolder.activeInHierarchy ||
            steakHolder.activeInHierarchy || stewHolder.activeInHierarchy || pancakeHolder.activeInHierarchy) 
            && !currentInteracTable.occupied)
        {
            currentInteracTable.EnableOutline();
        }
        else
        {
            currentInteracTable.DisableOutline();
        }
        
    }

    void SetNewCurrentClueInteractable(ClueInteractable newClueInteractable)
    {
        currentClueInteractable = newClueInteractable;
        currentClueInteractable.EnableOutline();
    }

    void SetNewCurrentDoorInteractable(DoorInteractable newDoorInteractable)
    {
        currentDoorInteractable = newDoorInteractable;
        currentDoorInteractable.EnableOutline();
    }

    void SetNewCurrentCompInteractable(ComputerInteractable newCompInteractable)
    {
        currentCompInteractable = newCompInteractable;
        currentCompInteractable.EnableOutline();
    }

    void SetNewCurrentPieceInteractable(PieceInteractable newPieceInteractable)
    {
        currentPieceInteractable = newPieceInteractable;

        if (pieceOneHolder.activeInHierarchy || pieceTwoHolder.activeInHierarchy || pieceThreeHolder.activeInHierarchy)
        {
            currentPieceInteractable.DisableOutline();
        }
        else
        {
            currentPieceInteractable.EnableOutline();
        }
    }

    void SetNewCurrentKeyInteractable(KeyInteractable newKeyInteractable)
    {
        currentKeyInteractable = newKeyInteractable;
        if (pieceOneHolder.activeInHierarchy || pieceTwoHolder.activeInHierarchy || pieceThreeHolder.activeInHierarchy
            || puzzScript.GetComponent<PuzzleComplete>().keyPuzzleComplete())
        {
            currentKeyInteractable.EnableOutline();
        }
        else
        {
            currentKeyInteractable.DisableOutline();
        }
    }

    void SetNewCurrentOvenInteractable(OvenInteractable newOvenInteractable)
    {
        currentOvenInteractable = newOvenInteractable;
        currentOvenInteractable.EnableOutline();
    }

    void SetNewCurrentFridgeInteractable(FridgeInteractable newFridgeInteractable)
    {
        currentFridgeInteractable = newFridgeInteractable;
        currentFridgeInteractable.EnableOutline();
    }

    void SetNewCurrentTNTInteractable(TNTInteractable newTNTInteractable)
    {
        currentTNTInteractable = newTNTInteractable;

        if (currentTNTInteractable.GetComponent<TNTInteractable>().isInteractable)
        {
            currentTNTInteractable.EnableOutline();
        }
        else
        {
            currentTNTInteractable.DisableOutline();
        }
    }

    void SetNewCurrentMatchInteractable(MatchInteractable newMatchInteractable)
    {
        currentMatchInteractable = newMatchInteractable;

        if (currentMatchInteractable.GetComponent<MatchInteractable>().isInteractable)
        {
            currentMatchInteractable.EnableOutline();
        }
        else
        {
            currentMatchInteractable.DisableOutline();
        }
    }

    void SetNewCurrentCrackInteractable(CrackInteractable newCrackInteractable)
    {
        currentCrackInteractable = newCrackInteractable;

        if (currentCrackInteractable.GetComponent<CrackInteractable>().isInteractable)
        {
            currentCrackInteractable.EnableOutline();
        }
        else
        {
            currentCrackInteractable.DisableOutline();
        }
    }

    void SetNewCurrentSafeInteractable(SafeInteractable newSafeInteractable)
    {
        currentSafeInteractable = newSafeInteractable;
        currentSafeInteractable.EnableOutline();
    }

    void SetNewCurrentNoteInteractable(NoteInteractable newNoteInteractable)
    {
        currentNoteInteractable = newNoteInteractable;
        currentNoteInteractable.EnableOutline();
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    void DisableCurrentInteracTable()
    {
        if (currentInteracTable)
        {
            currentInteracTable.DisableOutline();
            currentInteracTable = null;
        }
    }

    void DisableCurrentClueInteractable()
    {
        if (currentClueInteractable)
        {
            currentClueInteractable.DisableOutline();
            currentClueInteractable = null;
        }
    }

    void DisableCurrentDoorInteractable()
    {
        if (currentDoorInteractable)
        {
            currentDoorInteractable.DisableOutline();
            currentDoorInteractable = null;
        }
    }

    void DisableCurrentCompInteractable()
    {
        if (currentCompInteractable)
        {
            currentCompInteractable.DisableOutline();
            currentCompInteractable = null;
        }
    }

    void DisableCurrentPieceInteractable()
    {
        if (currentPieceInteractable)
        {
            currentPieceInteractable.DisableOutline();
            currentPieceInteractable = null;
        }
    }

    void DisableCurrentKeyInteractable()
    {
        if (currentKeyInteractable)
        {
            currentKeyInteractable.DisableOutline();
            currentKeyInteractable = null;
        }
    }

    void DisableCurrentOvenInteractable()
    {
        if (currentOvenInteractable)
        {
            currentOvenInteractable.DisableOutline();
            currentOvenInteractable = null;
        }
    }

    void DisableCurrentFridgeInteractable()
    {
        if (currentFridgeInteractable)
        {
            currentFridgeInteractable.DisableOutline();
            currentFridgeInteractable = null;
        }
    }

    void DisableCurrentTNTInteractable()
    {
        if (currentTNTInteractable)
        {
            currentTNTInteractable.DisableOutline();
            currentTNTInteractable = null;
        }
    }

    void DisableCurrentMatchInteractable()
    {
        if (currentMatchInteractable)
        {
            currentMatchInteractable.DisableOutline();
            currentMatchInteractable = null;
        }
    }

    void DisableCurrentSafeInteractable()
    {
        if (currentSafeInteractable)
        {
            currentSafeInteractable.DisableOutline();
            currentSafeInteractable = null;
        }
    }

    void DisableCurrentNoteInteractable()
    {
        if (currentNoteInteractable)
        {
            currentNoteInteractable.DisableOutline();
            currentNoteInteractable = null;
        }
    }

    void DisableCurrentCrackInteractable()
    {
        if (currentCrackInteractable)
        {
            currentCrackInteractable.DisableOutline();
            currentCrackInteractable = null;
        }
    }

    private IEnumerator NoDoorEntry ()
    {
        if (!wrongBuzzer.isPlaying)
        {
            wrongBuzzer.Play();
        }
        yield return new WaitForSeconds(0.5f);

        if (wrongBuzzer.isPlaying)
        {
            wrongBuzzer.Stop();
        }
        yield return new WaitForSeconds(4.5f);
        noEntryMessage.SetActive(false);
    }
}
