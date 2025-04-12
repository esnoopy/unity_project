using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public void Interact(GameObject chatBox){

        if(chatBox != null){
            chatBox.SetActive(true);
        }
    }
}
