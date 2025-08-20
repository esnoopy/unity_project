/*using System.Collections;
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
            playerInRange = true;
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
            playerInRange = false;
            dialoguePanel.SetActive(false);

            if (dialogueManager != null)
            {
                dialogueManager.EndDialogue();
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;        // Reference to your dialogue panel (can be hidden at start)
    public GameObject pressEText;           // Optional: UI Text or TMP object that says "Press E to talk"

    private Dialogue dialogueData;
    private DialogueManager dialogueManager;

    private bool playerInRange = false;
    private bool dialogueStarted = false;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (pressEText != null)
            pressEText.SetActive(false);

        dialogueManager = FindFirstObjectByType<DialogueManager>();
        dialogueData = GetComponent<Dialogue>();

        if (dialogueManager == null)
            Debug.LogError("DialogueManager not found in scene!");

        if (dialogueData == null)
            Debug.LogError("Dialogue component not found on this object!");
    }

    void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed - Starting dialogue");

            if (dialogueManager != null && dialogueData != null)
            {
                dialoguePanel.SetActive(true);
                dialogueManager.StartDialogue(dialogueData);
                dialogueStarted = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dialogue trigger");
            playerInRange = true;

            if (pressEText != null)
                pressEText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited dialogue trigger");
            playerInRange = false;

            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            if (pressEText != null)
                pressEText.SetActive(false);

            if (dialogueManager != null)
                dialogueManager.EndDialogue();

            dialogueStarted = false;
        }
    }
}
