using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // Reference to your UI panel with text
    private Dialogue dialogueData;
    private DialogueManager dialogueManager;

    private bool playerInRange = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueData = GetComponent<Dialogue>();

        if (dialogueManager == null)
            Debug.LogError("DialogueManager not found in scene!");

        if (dialogueData == null)
            Debug.LogError("Dialogue component not found on this object!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dialogue trigger");
            dialoguePanel.SetActive(true);

            if (dialogueManager != null && dialogueData != null)
            {
                dialogueManager.StartDialogue(dialogueData);
                Debug.Log("Dialogue started for: " + dialogueData.name);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited dialogue trigger");
            dialoguePanel.SetActive(false);

            if (dialogueManager != null)
            {
                dialogueManager.EndDialogue();
            }
        }
    }
}