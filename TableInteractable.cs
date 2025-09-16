using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TableInteractable : MonoBehaviour
{
    Outline outline;

    public GameObject burger;
    public GameObject burgerHolder;

    public GameObject whiskey;
    public GameObject whiskeyHolder;

    public GameObject hotdog;
    public GameObject hotdogHolder;

    public GameObject pizza;
    public GameObject pizzaHolder;

    public GameObject steak;
    public GameObject steakHolder;

    public GameObject stew;
    public GameObject stewHolder;

    public GameObject pancake;
    public GameObject pancakeHolder;

    public bool occupied;

    public UnityEvent onInteraction;
    // Start is called before the first frame update
    void Start()
    {
        burger.SetActive(false);
        whiskey.SetActive(false);
        hotdog.SetActive(false);
        pizza.SetActive(false);
        steak.SetActive(false);
        stew.SetActive(false);
        pancake.SetActive(false);

        outline = GetComponent<Outline>();
        DisableOutline();
        occupied = false;
    }
    private void Update()
    {
        if (!(burger.activeInHierarchy || whiskey.activeInHierarchy || hotdog.activeInHierarchy || pizza.activeInHierarchy
            || stew.activeInHierarchy || steak.activeInHierarchy || pancake.activeInHierarchy))
        {
            occupied = false;
        }
        else
        {
            DisableOutline();
        }
        
    }
    public void Interact()
    {
        if (burgerHolder.activeInHierarchy && occupied == false)
        {
            
                onInteraction.Invoke();
                burger.SetActive(true);
                burgerHolder.SetActive(false);
                occupied = true;
                
        }
        else if (burgerHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (whiskeyHolder.activeInHierarchy && occupied == false)
        {
            
                onInteraction.Invoke();
                whiskey.SetActive(true);
                whiskeyHolder.SetActive(false);
                occupied = true;
                
        }
        else if (whiskeyHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (hotdogHolder.activeInHierarchy && occupied == false)
        {

            onInteraction.Invoke();
            hotdog.SetActive(true);
            hotdogHolder.SetActive(false);
            occupied = true;

        }
        else if (hotdogHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (pizzaHolder.activeInHierarchy && occupied == false)
        {

            onInteraction.Invoke();
            pizza.SetActive(true);
            pizzaHolder.SetActive(false);
            occupied = true;

        }
        else if (pizzaHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (steakHolder.activeInHierarchy && occupied == false)
        {

            onInteraction.Invoke();
            steak.SetActive(true);
            steakHolder.SetActive(false);
            occupied = true;

        }
        else if (steakHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (stewHolder.activeInHierarchy && occupied == false)
        {

            onInteraction.Invoke();
            stew.SetActive(true);
            stewHolder.SetActive(false);
            occupied = true;

        }
        else if (stewHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else if (pancakeHolder.activeInHierarchy && occupied == false)
        {

            onInteraction.Invoke();
            pancake.SetActive(true);
            pancakeHolder.SetActive(false);
            occupied = true;

        }
        else if (pancakeHolder.activeInHierarchy && occupied == true)
        {
            DisableOutline();
        }
        else
        {

        }

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
