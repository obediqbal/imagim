using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Tower")]
public class WarriorStateTower : WarriorStateManager
{
    Transform tower;
    WarriorUnit warrior;

    public override void OnEnter(WarriorUnit _warrior)
    {
        tower = GameObject.FindWithTag("EnemyTower").transform;
        warrior = _warrior;
        warrior.lockedEnemy = tower;
    }
    public override void Update(WarriorUnit warrior)
    {
        if (tower != null) {
            warrior.ChaseTarget(tower);
        }
    }

    public override void HandleStateSwitching(WarriorUnit warrior){
        if (warrior.TargetOnAttackRange())
        {
            warrior.SwitchState(warrior.Attacking);
        }
        if (warrior.enemiesInChaseRange.Count > 0) 
        {
            warrior.lockedEnemy = warrior.enemiesInChaseRange[0];
            warrior.SwitchState(warrior.TargetClosest);
        }
    }
}
