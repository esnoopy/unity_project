using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI paragraphText;
    public Book book;

    public void StartInfoDialogue(InfoDialogue infoinfo)
    {

        paragraphText.text = infoinfo.paragraph;
    }

    public void EndInfoDialogue()
    {
        Debug.Log("End of conversation");
    }

    public void SendToBook()
    {
        book.saveToBook(paragraphText);
    }
}

