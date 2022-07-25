using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTargetClosest : WarriorBaseState
{
    Transform target, player;
    Rigidbody2D rb;

    public override void OnEnter(WarriorController warrior)
    {
        player = GameObject.FindWithTag("Enemy").transform;
        rb = warrior.GetComponent<Rigidbody2D>();

        ChangeTarget(player);
    }
    public override void OnTriggerEnter2D(WarriorController warrior, Collider2D collider)
    {
        if (collider.tag == "Enemy") // Or any deployable units
        {
            warrior.enemyInArea += 1;
        }
    }

    public override void OnTriggerExit2D(WarriorController warrior, Collider2D collider)
    {
        if (collider.tag == "Player") // Or any deployable units
        {
            warrior.enemyInArea -= 1;
        }
    }

    void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    void ChaseTarget(WarriorController warrior)
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

    public override void Update(WarriorController warrior)
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

    bool OnAttackRange(WarriorController warrior)
    {
        Collider2D hitEnemy = Physics2D.OverlapCircle(warrior.attackPosition.position, 0.5f * warrior.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            return hitEnemy.gameObject == target.gameObject;
        }
        else
        {
            return false;
        }

    }
}
