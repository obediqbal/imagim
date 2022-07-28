using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State Manager")]
public abstract class ZombieStateManager
{
    public abstract void OnEnter(ZombieUnit zombie);
    public abstract void Update(ZombieUnit zombie);
    public abstract void OnTriggerEnter2D(ZombieUnit zombie, Collider2D collider);
    public abstract void OnTriggerExit2D(ZombieUnit zombie, Collider2D collider);

}