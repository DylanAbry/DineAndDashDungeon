using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadInput : MonoBehaviour
{
    public TMP_InputField inputField;

    public AudioSource wrongBuzzer;
    public AudioSource correct;

    public GameObject wall;
    public Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        wall.GetComponent<Collider>().isTrigger = false;
        outline.enabled = false;

        inputField.text = "";
        // Add listener to input field to handle submission
        inputField.onEndEdit.AddListener(ReadStringInput);
        
    }

    // This method gets triggered when input is submitted
    public void ReadStringInput(string s)
    {
        string input = s.ToUpper(); // Convert input to uppercase to ignore case sensitivity

        if (input == "RECEIPT")
        {
            correct.Play();
            wall.GetComponent<Collider>().isTrigger = true; // Make the wall passable
            outline.enabled = true; // Enable the outline
            
        }
        else if (Input.GetButtonDown("SpaceBar"))
        {
            inputField.text = "";
        }
        else if (!(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cancel") || Input.GetButtonDown("SpaceBar")))
        {
            wrongBuzzer.Play();
            inputField.text = "";
        }
    }
}
