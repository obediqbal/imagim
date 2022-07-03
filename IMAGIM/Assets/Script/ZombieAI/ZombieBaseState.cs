using UnityEngine;

public abstract class ZombieBaseState 
{
    public abstract void OnEnter(ZombieController zombie);
    public abstract void Update(ZombieController zombie);
    public abstract void OnTriggerEnter2D(ZombieController zombie, Collider2D collider);
    public abstract void OnTriggerExit2D(ZombieController zombie, Collider2D collider);

}
