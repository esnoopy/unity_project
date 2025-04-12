using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour{

//https://docs.unity3d.com/Manual/DeactivatingGameObjects.html#:~:text=To%20deactivate%20a%20GameObject%20through,attached%20to%20it%20are%20stopped.
    public GameObject chatBox;
    private void Update(){

        if(Input.GetKeyDown(KeyCode.N)){
            float interactRange = 2f;
            Time.timeScale = 0f;
            //returns collider that overlap physics area and see if there are npcs
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray){
                if(collider.TryGetComponent(out NPCInteractable npcInteractable)){
                    npcInteractable.Interact(chatBox);
                }                
            }
        }else if(Input.GetKeyDown(KeyCode.E)){
            Time.timeScale = 1f;
            chatBox.SetActive(false);
        }
    }
}
