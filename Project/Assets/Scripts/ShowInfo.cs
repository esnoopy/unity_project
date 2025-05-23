using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public GameObject infoPanel;
    private InfoDialogue infoinfo;
    private InfoDialogueManager infomanager;

    void Start()
    {
        infoPanel.SetActive(false);
        Debug.Log("SET FALSE");
        infomanager = FindFirstObjectByType<InfoDialogueManager>();
        infoinfo = GetComponent<InfoDialogue>();

        if (infomanager == null)
        {
            Debug.Log("InfoDialogueManager not found in scene!");
        }
    }

    void OnTriggerEnter(Collider thing)
    {
        if (thing.tag == "Player")
        {
            Debug.Log("Player inside");
            infoPanel.SetActive(true);
            Debug.Log("SET True");
            if (infoinfo != null)
            {
                Debug.Log("InfoInfo not set.");   
            }
            if (infomanager != null && infoinfo != null)
            {
                infomanager.StartInfoDialogue(infoinfo);
                Debug.Log("InfoInfo not set."+infoinfo); 
            }
        }
    }

    void OnTriggerExit(Collider thing)
    {
        if(thing.tag == "Player"){
            Debug.Log("Player outside");
            infoPanel.SetActive(false);
            if (infomanager != null && infoinfo != null)
            {
                infomanager.EndInfoDialogue();
            }
        }
    }
}
