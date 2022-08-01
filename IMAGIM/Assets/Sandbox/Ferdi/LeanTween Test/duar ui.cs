using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class duarui : MonoBehaviour
{
    [SerializeField] GameObject NewGamePrompt;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ExitPrompt;

    public void NewGame()
    {
        NewGamePrompt.transform.LeanMoveLocal(new Vector2(0, 0), 1).setEaseInSine();
    }

    public void Settings()
    {
        SettingsMenu.transform.LeanMoveLocal(new Vector2(0, 0), 1).setEaseInSine();
    }
    public void ExitGame()
    {
        ExitPrompt.transform.LeanMoveLocal(new Vector2(0, 0), 1).setEaseInSine();
    }
    
}
