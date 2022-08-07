using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State Manager")]
public abstract class ZombieStateManager
{
    public abstract void OnEnter(ZombieUnit zombie);
    public abstract void Update(ZombieUnit zombie);
    public abstract void HandleStateSwitching(ZombieUnit zombie);
}