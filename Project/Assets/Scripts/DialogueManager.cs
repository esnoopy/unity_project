using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> lines;
    private string currentLine;
    public void Start(){
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        lines = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){

        nameText.text = dialogue.name;
        lines.Clear();

        foreach(string line in dialogue.lines){
            Debug.Log("Enqueue line: " + line);
            lines.Enqueue(line);
        }

        DisplayLine(lines.Dequeue());
    }

    private void DisplayLine(string line)  ///ADD ENTER CLICK
    {
        currentLine = line;
        dialogueText.text = currentLine;
    }

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

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DisplayLine(lines.Dequeue());
    }
    public void EndDialogue(){
        Debug.Log("End of conversation");
    }
}
