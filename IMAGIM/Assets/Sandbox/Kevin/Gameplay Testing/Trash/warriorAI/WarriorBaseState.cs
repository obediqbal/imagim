using UnityEngine;

public abstract class WarriorBaseState
{
    public abstract void OnEnter(WarriorController warrior);
    public abstract void Update(WarriorController warrior);
    public abstract void OnTriggerEnter2D(WarriorController warrior, Collider2D collider);
    public abstract void OnTriggerExit2D(WarriorController warrior, Collider2D collider);

}
