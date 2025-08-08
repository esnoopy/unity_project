using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI nameText;
    public Image dialogueImage;

    private Queue<Sprite> spriteQueue;
    private Sprite currentSprite;
    public void Start(){
        spriteQueue = new Queue<Sprite>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.name;
        spriteQueue.Clear();

        foreach (Sprite sprite in dialogue.dialogueSprites)
        {
            spriteQueue.Enqueue(sprite);
        }

        //DisplayLine(lines.Dequeue());
        DisplayNextSprite();
    }

    /*private void DisplayLine(string line)  ///ADD ENTER CLICK
    {
        currentLine = line;
        dialogueText.text = currentLine;
    }*/

    /*public void DisplayNextLine(){
        if(lines.Count == 0){
            Debug.Log("No more lines.");
            EndDialogue();
            return;
        }

        string line = lines.Dequeue();
        Debug.Log("Displaying line: " + line);
        dialogueText.text = line;
    }*/

    /*public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DisplayLine(lines.Dequeue());
    }*/

    public void DisplayNextSprite()
    {
        if (spriteQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DisplaySprite(spriteQueue.Dequeue());
    }

    private void DisplaySprite(Sprite sprite)
    {
        currentSprite = sprite;
        dialogueImage.sprite = currentSprite;
    }
    public void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        Debug.Log("End of conversation");
    }
}
