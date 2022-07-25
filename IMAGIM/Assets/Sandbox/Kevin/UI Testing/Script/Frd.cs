using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frd : MonoBehaviour
{
    [SerializeField] GameObject NewGamePrompt;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ExitPrompt;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsMenu.transform.LeanMoveLocal(new Vector2(-647, 0), 1).setEaseInSine();
            NewGamePrompt.transform.LeanMoveLocal(new Vector2(-349, 0), 1).setEaseInSine();
            ExitPrompt.transform.LeanMoveLocal(new Vector2(-349, 0), 1).setEaseInSine();
        }
    }
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
