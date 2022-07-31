using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Chase")]
public class WarriorStateChase : WarriorStateManager
{   
    Transform target, player;
    Rigidbody2D rb;

    public override void OnEnter(WarriorUnit warrior)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = warrior.GetComponent<Rigidbody2D>();
        ChangeTarget(warrior.lockedEnemy);
    }
    public override void OnTriggerEnter2D(WarriorUnit warrior, Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) // Or any deployable units
        {
            warrior.enemyInArea += 1;
            if (warrior.lockedEnemy == null)
            {
                warrior.lockedEnemy = collider.transform;
            }
            warrior.enemiesInRange.Add(collider.transform);
        }
    }

    public override void OnTriggerExit2D(WarriorUnit warrior, Collider2D collider)
    {
        if (collider.CompareTag("Player") || (collider.CompareTag("Ally"))) // Or any deployable units
        {
            warrior.enemyInArea -= 1;
            if (warrior.lockedEnemy == collider.transform || warrior.lockedEnemy.gameObject.activeInHierarchy == false)
            {
                warrior.RetargetEnemy();
            }
            warrior.enemiesInRange.Remove(collider.transform);
            if (warrior.enemiesInRange.Count > 0)
            {
                warrior.lockedEnemy = warrior.enemiesInRange[0];

            }
            else
            {
                warrior.RetargetEnemy();
            }
        }
    }

    void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    void ChaseTarget(WarriorUnit warrior)
    {
        if (target != null)
        {
            float distanceToTarget = target.position.x - warrior.transform.position.x;
            Vector3 direction = distanceToTarget < 0 ? Vector3.left : Vector3.right;
            warrior.xOrientation = direction.x;
            warrior.transform.localScale = warrior.xOrientation < 0 ? new Vector3(-5, 5, 1) : new Vector3(5, 5, 1);

            rb.velocity = direction * warrior.speed;
        }
    }

    public override void Update(WarriorUnit warrior)
    {
        if (warrior.enemyInArea == 0)
        {
            warrior.SwitchState(warrior.TargetTower);
        }

        if (OnAttackRange(warrior))
        {
            warrior.SwitchState(warrior.Attacking);
        }

        ChaseTarget(warrior);
    }

    bool OnAttackRange(WarriorUnit warrior)
    {
        Collider2D hitEnemy = Physics2D.OverlapCircle(warrior.attackPosition.position, 0.5f * warrior.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            return hitEnemy.gameObject == target.gameObject;
        } else {
            return false;
        }
        
    }

}
