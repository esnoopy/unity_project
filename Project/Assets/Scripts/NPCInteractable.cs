using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    //private Animator animator;
    public void Interact(Transform interactTransform){
        Debug.Log("?????????????????????????????????????????????????");
        var dialogueManager = FindFirstObjectByType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("DialogueManager not found in scene!");
        }
    }

    /*public string GetInteractText(){
        return dialogue != null && dialogue.lines.Length > 0 ? dialogue.lines[0] : "Talk";
    }*/

    public Transform GetTransform(){
        return transform;
    }
}
