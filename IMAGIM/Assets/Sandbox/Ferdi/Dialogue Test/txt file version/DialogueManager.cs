using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject CanvasBox;
    public Text TextBox;
    public Text NameText;
    public GameObject FastToggletext;
    [HideInInspector]
    public bool checking = false;
    public string check = "";
    public string size = "";
    public bool bold = false;
    public bool italic = false;
    public bool customSize = false;
    private string Line;
    private string currSetting = "";
    private bool Typing = false;
    private bool FastToggle = false;
    private Queue<string> DialogueQueue = new Queue<string>();
    private GameObject currCharRight;
    private GameObject currCharLeft;
    private GameObject currCharMid;
    // Start is called before the first frame update
    void Start()
    {
        CanvasBox.SetActive(false);
        FastToggletext.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (FastToggle)
            {
                FastToggle = false;
                FastToggletext.SetActive(false);
            } 
            else
            {
                FastToggle = true;
                FastToggletext.SetActive(true);

            }
        }
    }

    public void StartDialogue(Queue<string> dialogue)
    {
        CanvasBox.SetActive(true);
        DialogueQueue = dialogue;
        PrintDialogue();
    }

    void PrintDialogue()
    {
        if (DialogueQueue.Peek().Contains("ENDQUEUE"))
        {
            DialogueQueue.Dequeue();
            EndDialogue();
        }
        else
        {
            if (DialogueQueue.Peek().Contains("[SETTING=") && !Typing)
            {
                string setting = DialogueQueue.Peek();
                setting = DialogueQueue.Dequeue().Substring(setting.IndexOf("=") + 1, setting.IndexOf("]") - (setting.IndexOf("=") + 1));
                if (currSetting != "")
                {
                    GameObject.Find("/Canvas/Backgrounds/" + currSetting + " BG").SetActive(false);
                }
                currSetting = setting;
                GameObject.Find("/Canvas/Backgrounds/" + setting + " BG").SetActive(true);
            }

            if (DialogueQueue.Peek().Contains("[NAME=") && !Typing)
            {
                string name = DialogueQueue.Peek();
                name = DialogueQueue.Dequeue().Substring(name.IndexOf("=") + 1, name.IndexOf("]") - (name.IndexOf("=") + 1));
                if (name == "null")
                {
                    NameText.text = "";
                }
                else
                {
                    NameText.text = name;
                }
            }

            if (DialogueQueue.Peek().Contains("[CHAR=") && !Typing)
            {
                string chara = DialogueQueue.Peek();
                if (chara.Substring(chara.IndexOf("=") + 1, chara.IndexOf("]") - (chara.IndexOf("=") + 1)) == "null")
                {
                    if (currCharLeft != null)
                    {
                        currCharLeft.SetActive(false);
                    }
                    if (currCharRight != null)
                    {
                        currCharRight.SetActive(false);
                    }
                    if (currCharMid != null)
                    {
                        currCharMid.SetActive(false);
                    }
                    DialogueQueue.Dequeue();
                }
                else
                {
                    string character;
                    string type;
                    string alignment;
                    character = chara.Substring(chara.IndexOf("=") + 1, chara.IndexOf("_") - (chara.IndexOf("=") + 1));
                    type = chara.Substring(chara.IndexOf("=") + 2 + character.Length , chara.LastIndexOf("_") - (chara.IndexOf("=") + 2 + character.Length));
                    alignment = chara.Substring(chara.LastIndexOf("_") + 1, chara.IndexOf("]") - chara.LastIndexOf("_") - 1);
                    DialogueQueue.Dequeue();
                    GameObject portrait = GameObject.Find("Canvas/Character Portraits/" + character + "/" + type);
                    if (alignment == "left")
                    {
                        if (currCharLeft != null)
                        {
                            currCharLeft.SetActive(false);
                        }
                        portrait.SetActive(true);
                        portrait.transform.localPosition = new Vector3(-200, -10, 0);
                        currCharLeft = portrait;
                    }
                    else if (alignment == "right")
                    {
                        if (currCharRight != null)
                        {
                            currCharRight.SetActive(false);
                        }
                        portrait.SetActive(true);
                        portrait.transform.localPosition = new Vector3(200, -10, 0);
                        currCharRight = portrait;
                    }
                    else
                    {
                        if (currCharMid != null)
                        {
                            currCharMid.SetActive(false);
                        }
                        portrait.SetActive(true);
                        portrait.transform.localPosition = new Vector3(0, -10, 0);
                        currCharMid = portrait;
                    }
                }
            }

            Line = DialogueQueue.Peek();
            if (!Typing)
            {
                StartCoroutine(TypeSentence(Line));
            }
            else
            {
                bold = false;
                italic = false;
                customSize = false;
                StopAllCoroutines();
                TextBox.text = Line;
                DialogueQueue.Dequeue();
                Typing = false;
            }
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        TextBox.text = "";
        Typing = true;
        foreach (char letter in sentence.ToCharArray())
        {
            if (letter.ToString() == "<")
            {
                check = "";
                checking = true;
            }
            if (letter.ToString() == ">")
            {
                check += letter;
                checking = false;
                Debug.Log(check);
                switch (check)
                {
                    case "<b>":
                        bold = true;
                        break;
                    case "</b>":
                        bold = false;
                        break;
                    case "<i>":
                        italic = true;
                        break;
                    case "</i>":
                        italic = false;
                        break;
                    default:
                        if (check.Contains("<size="))
                        {
                            customSize = true;
                            size = check;
                        }
                        else if (check.Contains("</size>"))
                        {
                            customSize = false;
                        }
                        break;
                }
            }

            if (checking)
            {
                check += letter;
            }
            else
            {
                string nextletter = letter.ToString();
                if (nextletter != ">")
                {
                    if (bold)
                    {
                        nextletter = "<b>" + nextletter + "</b>";
                    }
                    if (italic)
                    {
                        nextletter = "<i>" + nextletter + "</i>";
                    }
                    if (customSize)
                    {
                        nextletter = size + nextletter + "</size>";
                    }
                    TextBox.text += nextletter;
                }
                if (!FastToggle)
                {
                    yield return new WaitForSeconds(.025f);
                }
                else
                {
                    yield return null;
                }
            }
        }
        DialogueQueue.Dequeue();
        Typing = false;
    }
    void EndDialogue()
    {
        TextBox.text = "";
        NameText.text = "";
        if (currCharLeft != null)
        {
            currCharLeft.SetActive(false);
        }
        if (currCharRight != null)
        {
            currCharRight.SetActive(false);
        }
        if (currCharMid != null)
        {
            currCharMid.SetActive(false);
        }
        DialogueQueue.Clear();
        CanvasBox.SetActive(false);
    }
}
