using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Chase")]
public class WarriorStateChase : WarriorStateManager
{   
    Transform target;

    public override void OnEnter(WarriorUnit warrior)
    {
        
    }

    public override void Update(WarriorUnit warrior)
    {
        if (!warrior.enemiesInChaseRange.Contains(warrior.lockedEnemy))
        {
            OnTargetExit(warrior);
        }

        if (warrior.lockedEnemy.transform != null)
        {
            warrior.ChaseTarget(warrior.lockedEnemy.transform);
        }
    }

    private void OnTargetExit(WarriorUnit warrior)
    {
        if (warrior.enemiesInChaseRange.Count > 0)
        {
            RaycastHit2D closestWarrior = warrior.enemiesInChaseRange[0];
            float shortestDist = Mathf.Infinity;
            if (closestWarrior)
            {
                shortestDist = Vector2.Distance(warrior.transform.position, closestWarrior.transform.position);
            }
            
            foreach (RaycastHit2D enemy in warrior.enemiesInChaseRange)
            {
                if (enemy)
                {
                    if (Vector2.Distance(warrior.transform.position, enemy.transform.position) < shortestDist)
                    {
                        closestWarrior = enemy;
                        shortestDist = Vector2.Distance(warrior.transform.position, enemy.transform.position);
                    }
                }
            }

            warrior.lockedEnemy = closestWarrior;
        }
        else
        {
            warrior.SwitchState(warrior.TargetTower);
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
