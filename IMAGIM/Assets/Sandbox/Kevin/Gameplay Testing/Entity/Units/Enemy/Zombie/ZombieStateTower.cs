using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Tower")]
public class ZombieStateTower : ZombieStateManager
{
    Transform tower;

    public override void OnEnter(ZombieUnit zombie)
    {
        tower = GameObject.FindWithTag("AllyTower").transform;
    }
    public override void Update(ZombieUnit zombie)
    {
        if (tower != null) {
            zombie.ChaseTarget(tower);
        }
    }

    public override void HandleStateSwitching(ZombieUnit zombie){
        if (zombie.TargetOnAttackRange())
        {
            zombie.SwitchState(zombie.Attacking);
        }
        if (zombie.enemiesInChaseRange.Count > 0) 
        {
            zombie.lockedEnemy = zombie.enemiesInChaseRange[0];
            zombie.SwitchState(zombie.TargetClosest);
        }
    }
}
