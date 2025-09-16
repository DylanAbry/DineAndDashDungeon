using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjects : MonoBehaviour
{

    public GameObject mealItem;
    public GameObject holder;

    public bool isUsed = false;

    // Update is called once per frame
    void Update()
    {
        if (holder.activeInHierarchy)
        {
            
            if (Input.GetButtonDown("Drop"))
            {
                mealItem.SetActive(true);
                holder.SetActive(false);
                isUsed = true;
            }
            
        }
    }
}
