using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDialogueManager : MonoBehaviour
{
    public Image displayImage;  

    public void StartInfoDialogue(InfoDialogue infoinfo){

    if (infoinfo == null)
    {
        Debug.LogError("infoinfo is NULL! Nothing was passed into StartInfoDialogue.");
        return;
    }

    if (infoinfo.image == null)
    {
        Debug.LogWarning("infoinfo is NOT null, but image is null.");
        return;
    }
        if (infoinfo.image != null)  // Check if the sprite is assigned
        {
            displayImage.sprite = infoinfo.image;  // Set the sprite to the image component
            displayImage.gameObject.SetActive(true); // Ensure the image GameObject is active
            Debug.Log("Sprite texture: " + infoinfo.image.texture);
        }
        else
        {
            Debug.LogWarning("Image is not assigned in InfoDialogue!");
        }
    }
    
    public void EndInfoDialogue()
    {
        Debug.Log("End of conversation");
    }
}

