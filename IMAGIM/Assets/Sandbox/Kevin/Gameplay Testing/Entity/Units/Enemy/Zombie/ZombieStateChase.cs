using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Chase")]
public class ZombieStateChase : ZombieStateManager
{   
    Transform target;

    public override void OnEnter(ZombieUnit zombie)
    {
        AssignTarget(zombie.lockedEnemy);
    }

    void AssignTarget(Transform _target)
    {
        target = _target;
    }

    public override void Update(ZombieUnit zombie)
    {
        if (target != null) 
        {
            zombie.ChaseTarget(target);
        }
        else
        {
            if (zombie.enemiesInChaseRange.Count > 0) 
            {
                zombie.lockedEnemy = zombie.enemiesInChaseRange[0];
                AssignTarget(zombie.lockedEnemy);
            } else {
                zombie.SwitchState(zombie.TargetTower);
            }
        }
    }

    public override void HandleStateSwitching(ZombieUnit zombie) 
    {
        if (zombie.enemiesInChaseRange.Count == 0) 
        {
            zombie.SwitchState(zombie.TargetTower);
        }

        if (zombie.TargetOnAttackRange()) 
        {
            zombie.SwitchState(zombie.Attacking);
        }
    }
}
