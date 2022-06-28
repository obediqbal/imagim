using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject gameTitle;
    [SerializeField] GameObject gameLogo;
    
    public void clickPlay()
    {
        gameTitle.transform.LeanMoveLocal(new Vector2(650, 60), 1).setEaseInSine();
        gameLogo.transform.LeanMoveLocal(new Vector2(650, -64), 1).setEaseInSine();
        playButton.transform.LeanMoveLocal(new Vector2(40, 89), 1).setEaseOutSine();
        settingsButton.transform.LeanMoveLocal(new Vector2(0, 30), 1).setEaseOutSine();
        creditsButton.transform.LeanMoveLocal(new Vector2(0, -32), 1).setEaseOutSine();
    }
    public void clickSettings()
    {
        gameTitle.transform.LeanMoveLocal(new Vector2(650, 60), 1).setEaseInSine();
        gameLogo.transform.LeanMoveLocal(new Vector2(650, -64), 1).setEaseInSine();
        playButton.transform.LeanMoveLocal(new Vector2(0, 89), 1).setEaseOutSine();
        settingsButton.transform.LeanMoveLocal(new Vector2(40, 30), 1).setEaseOutSine();
        creditsButton.transform.LeanMoveLocal(new Vector2(0, -32), 1).setEaseOutSine();
    }
    public void clickCredits()
    {
        gameTitle.transform.LeanMoveLocal(new Vector2(650, 60), 1).setEaseInSine();
        gameLogo.transform.LeanMoveLocal(new Vector2(650, -64), 1).setEaseInSine();
        playButton.transform.LeanMoveLocal(new Vector2(0, 89), 1).setEaseOutSine();
        settingsButton.transform.LeanMoveLocal(new Vector2(0, 30), 1).setEaseOutSine();
        creditsButton.transform.LeanMoveLocal(new Vector2(40, -32), 1).setEaseOutSine();
    }
    public void clickExit()
    {
        gameTitle.transform.LeanMoveLocal(new Vector2(128, 60), 1).setEaseInSine();
        gameLogo.transform.LeanMoveLocal(new Vector2(128, -64), 1).setEaseInSine();
        playButton.transform.LeanMoveLocal(new Vector2(0, 89), 1).setEaseOutSine();
        settingsButton.transform.LeanMoveLocal(new Vector2(0, 30), 1).setEaseOutSine();
        creditsButton.transform.LeanMoveLocal(new Vector2(0, -32), 1).setEaseOutSine();
    }
}
