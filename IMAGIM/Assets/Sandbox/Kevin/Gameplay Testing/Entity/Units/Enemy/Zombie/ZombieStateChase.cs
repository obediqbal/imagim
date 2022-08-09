using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Chase")]
public class ZombieStateChase : ZombieStateManager
{   
    Transform target;

    public override void OnEnter(ZombieUnit zombie)
    {
        
    }

    public override void Update(ZombieUnit zombie)
    {
        if (!zombie.enemiesInChaseRange.Contains(zombie.lockedEnemy))
        {
            OnTargetExit(zombie);
        }

        if (zombie.lockedEnemy.transform != null)
        {
            zombie.ChaseTarget(zombie.lockedEnemy.transform);
        }
    }

    private void OnTargetExit(ZombieUnit zombie)
    {
        if (zombie.enemiesInChaseRange.Count > 0)
        {
            RaycastHit2D closestZombie = zombie.enemiesInChaseRange[0];
            float shortestDist = Mathf.Infinity;
            if (closestZombie)
            {
                shortestDist = Vector2.Distance(zombie.transform.position, closestZombie.transform.position);
            }
            foreach (RaycastHit2D enemy in zombie.enemiesInChaseRange)
            {
                if (enemy)
                {
                    if (Vector2.Distance(zombie.transform.position, enemy.transform.position) < shortestDist)
                    {
                        closestZombie = enemy;
                        shortestDist = Vector2.Distance(zombie.transform.position, enemy.transform.position);
                    }
                }
            }

            zombie.lockedEnemy = closestZombie;
        }
        else
        {
            zombie.SwitchState(zombie.TargetTower);
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
