using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPromptTrigger : MonoBehaviour
{
    public GameObject objectToToggle; // Assign this in the Inspector
    private TextWriter textWriter;

    private void Start() {
        textWriter = objectToToggle.GetComponent<TextWriter>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the entering object is the player (or whatever tag you use)
        {
            objectToToggle.SetActive(true); // Enable the object
            textWriter.WriteInstruction(textWriter.testMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToToggle.SetActive(false); // Disable the object
        }
    }
}