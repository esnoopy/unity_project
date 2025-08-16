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
    public AudioSource audioSource;

    private Queue<Sprite> spriteQueue;
    private Queue<AudioClip> audioQueue;
    private Sprite currentSprite;
    public void Start()
    {
        spriteQueue = new Queue<Sprite>();
        audioQueue = new Queue<AudioClip>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.name;
        spriteQueue.Clear();
        audioQueue.Clear();

        /*foreach (Sprite sprite in dialogue.dialogueSprites)
        {
            spriteQueue.Enqueue(sprite);
            if (i < dialogue.dialogueAudioClips.Length)
            {
                audioQueue.Enqueue(dialogue.audioClips[i]); // Enqueue corresponding audio clip
            }
            else
            {
                audioQueue.Enqueue(null); // If there's no audio, enqueue a null
            }
        }*/

        for (int i = 0; i < dialogue.dialogueSprites.Length; i++)
        {
            spriteQueue.Enqueue(dialogue.dialogueSprites[i]);
            if (i < dialogue.audioClips.Length)
            {
                audioQueue.Enqueue(dialogue.audioClips[i]); // Enqueue corresponding audio clip
            }
            else
            {
                audioQueue.Enqueue(null); // If there's no audio, enqueue a null
            }
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
        PlayNextAudio();
    }

    private void DisplaySprite(Sprite sprite)
    {
        currentSprite = sprite;
        dialogueImage.sprite = currentSprite;
    }

    private void PlayNextAudio()
    {
        if (audioQueue.Count > 0)
        {
            AudioClip clip = audioQueue.Dequeue(); // Dequeue the next audio clip
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();  // Play the audio clip
            }
        }
    }
    public void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        Debug.Log("End of conversation");
    }
}
