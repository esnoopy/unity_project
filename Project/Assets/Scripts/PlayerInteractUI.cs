using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;

    private void Update(){
        if(playerInteract.GetInteractableObject() != null){
            Show(/*playerInteract.GetInteractableObject()*/);
        }else{
            Hide();
        }
    }

    public void Show(/*IInteractable interactable*/){
        if (!containerGameObject.activeSelf){
            containerGameObject.SetActive(true);
        }
    }

    public void SetDialogueText(string line){
        if (!containerGameObject.activeSelf){
            containerGameObject.SetActive(true);
        }
    }

    public void Hide(){
        containerGameObject.SetActive(false);
    }
}
