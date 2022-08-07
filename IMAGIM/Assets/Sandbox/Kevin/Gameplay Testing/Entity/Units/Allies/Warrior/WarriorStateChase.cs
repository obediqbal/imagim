using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Chase")]
public class WarriorStateChase : WarriorStateManager
{   
    Transform target;

    public override void OnEnter(WarriorUnit warrior)
    {
        AssignTarget(warrior.lockedEnemy);
    }

    void AssignTarget(Transform _target)
    {
        target = _target;
    }

    public override void Update(WarriorUnit warrior)
    {
        if (target != null) 
        {
            warrior.ChaseTarget(target);
        }
        else
        {
            if (warrior.enemiesInChaseRange.Count > 0) 
            {
                warrior.lockedEnemy = warrior.enemiesInChaseRange[0];
                AssignTarget(warrior.lockedEnemy);
            } else {
                warrior.SwitchState(warrior.TargetTower);
            }
        }
    }

    public override void HandleStateSwitching(WarriorUnit warrior) 
    {
        if (warrior.enemiesInChaseRange.Count == 0) 
        {
            warrior.SwitchState(warrior.TargetTower);
        }

        if (warrior.TargetOnAttackRange()) 
        {
            warrior.SwitchState(warrior.Attacking);
        }
    }

}
