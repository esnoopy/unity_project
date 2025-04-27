using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable lastInteracted;

    private void Update(){
        //returns the collectors that overlap the area
        //hit all objects and layers, cycle them and check which ones are nps return array of colliders
        //pressing e getting all the collectors
        
        //if(Input.GetKeyDown(KeyCode.E)){
            IInteractable interactable = GetInteractableObject();
            if(interactable != null  && interactable != lastInteracted){
                interactable.Interact(transform);
                lastInteracted = interactable;
            }

            // Reset if player moves away
            if (interactable == null)
            {
                lastInteracted = null;
            }
            /*float interactRange = 5f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray){
                //if true then collider has an npc interactable
                if(collider.TryGetComponent(out IInteractable interactable)){
                    Debug.Log("NPC detected: " + interactable.gameObject.name);
                    interactable.Interact();
                }
            }*/
        //}

    }

    public IInteractable GetInteractableObject(){
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderArray){
            //if true then collider has an npc interactable
            if(collider.TryGetComponent(out IInteractable interactable)){
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach(IInteractable interactable in interactableList){
            if(closestInteractable == null){
                closestInteractable = interactable;
            }else{
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position)){
                    //Closer
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }
    
}
