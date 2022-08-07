using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State Manager")]
public abstract class WarriorStateManager
{
    public abstract void OnEnter(WarriorUnit warrior);
    public abstract void Update(WarriorUnit warrior);
    public abstract void HandleStateSwitching(WarriorUnit warrior);
}