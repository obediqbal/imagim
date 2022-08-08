using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset TextFile;
    private Queue<string> dialogue = new Queue<string>();
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        ReadTextFile();
        dialogueManager.StartDialogue(dialogue);
    }

    void ReadTextFile()
    {
        string txt = TextFile.text;
        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray());

        foreach (string line in lines)
        {
            if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
            {
                if (line.StartsWith("["))
                {
                    string special = line.Substring(0, line.IndexOf("]") + 1);
                    string curr = line.Substring(line.IndexOf("]") + 1);
                    while (curr.Contains("["))
                    {
                        dialogue.Enqueue(special);
                        Debug.Log(curr);
                        special = curr.Substring(0, curr.IndexOf("]")+1);
                        curr = curr.Substring(curr.IndexOf("]")+1);
                    }
                    dialogue.Enqueue(special);
                    dialogue.Enqueue(curr);
                } 
                else
                {
                    dialogue.Enqueue(line);
                }
            }
        }
        dialogue.Enqueue("ENDQUEUE");
    }


}
