using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject CanvasBox;
    public Text TextBox;
    public Text NameText;
    private Queue<string> DialogueQueue = new Queue<string>();
    // Start is called before the first frame update
    void Start()
    {
        CanvasBox.SetActive(false);
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        CanvasBox.SetActive(true);
        DialogueQueue = dialogue;
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (DialogueQueue.Peek().Contains("EndQueue"))
        {
            DialogueQueue.Dequeue();
            EndDialogue();
        } else if (DialogueQueue.Peek().Contains("[NAME="))
        {
            string name = DialogueQueue.Peek();
            name = DialogueQueue.Dequeue().Substring(name.IndexOf("=") + 1, name.IndexOf("]") - (name.IndexOf("=") + 1));
            NameText.text = name;
            PrintDialogue();
        } else
        {
            TextBox.text = DialogueQueue.Dequeue();
        }
    }
    // Update is called once per frame
    void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        DialogueQueue.Clear();
        CanvasBox.SetActive(false);
    }
}
