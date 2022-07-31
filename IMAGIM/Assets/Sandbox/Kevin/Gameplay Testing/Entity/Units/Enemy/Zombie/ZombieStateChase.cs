using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Chase")]
public class ZombieStateChase : ZombieStateManager
{   
    Transform target, player;
    Rigidbody2D rb;

    public override void OnEnter(ZombieUnit zombie)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = zombie.GetComponent<Rigidbody2D>();
        ChangeTarget(zombie.lockedEnemy);
    }
    public override void OnTriggerEnter2D(ZombieUnit zombie, Collider2D collider)
    {
        if (collider.CompareTag("Player") || (collider.CompareTag("Ally"))) // Or any deployable units
        {
            zombie.enemyInArea += 1;
            if (zombie.lockedEnemy == null)
            {
                zombie.lockedEnemy = collider.transform;
            }
            zombie.enemiesInRange.Add(collider.transform);
        }
    }

    public override void OnTriggerExit2D(ZombieUnit zombie, Collider2D collider)
    {
        if (collider.CompareTag("Player") || (collider.CompareTag("Ally"))) // Or any deployable units
        {
            zombie.enemyInArea -= 1;
            if (zombie.lockedEnemy == collider.transform || zombie.lockedEnemy.gameObject.activeInHierarchy == false)
            {
                zombie.RetargetEnemy();
            }
            zombie.enemiesInRange.Remove(collider.transform);
            if (zombie.enemiesInRange.Count > 0)
            {
                zombie.lockedEnemy = zombie.enemiesInRange[0];

            }
            else
            {
                zombie.RetargetEnemy();
            }
        }
    }

    void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    void ChaseTarget(ZombieUnit zombie)
    {
        if (target != null)
        {
            float distanceToTarget = target.position.x - zombie.transform.position.x;
            Vector3 direction = distanceToTarget < 0 ? Vector3.left : Vector3.right;
            zombie.xOrientation = direction.x;
            zombie.transform.localScale = zombie.xOrientation < 0 ? new Vector3(-5, 5, 1) : new Vector3(5, 5, 1);

            rb.velocity = direction * zombie.speed;
        }
    }

    public override void Update(ZombieUnit zombie)
    {
        if (zombie.enemyInArea == 0)
        {
            zombie.SwitchState(zombie.TargetTower);
        }

        if (OnAttackRange(zombie))
        {
            zombie.SwitchState(zombie.Attacking);
        }

        ChaseTarget(zombie);
    }

    bool OnAttackRange(ZombieUnit zombie)
    {
        Collider2D hitEnemy = Physics2D.OverlapCircle(zombie.attackPosition.position, 0.5f * zombie.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            return hitEnemy.gameObject == target.gameObject;
        } else {
            return false;
        }
        
    }

}
