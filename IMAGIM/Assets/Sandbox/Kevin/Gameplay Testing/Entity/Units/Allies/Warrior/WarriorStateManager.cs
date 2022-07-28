using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State Manager")]
public abstract class WarriorStateManager
{
    public abstract void OnEnter(WarriorUnit warrior);
    public abstract void Update(WarriorUnit warrior);
    public abstract void OnTriggerEnter2D(WarriorUnit warrior, Collider2D collider);
    public abstract void OnTriggerExit2D(WarriorUnit warrior, Collider2D collider);

}