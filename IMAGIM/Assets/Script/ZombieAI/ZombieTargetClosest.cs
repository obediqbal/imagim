using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetClosest : ZombieBaseState
{   
    Transform target, player;
    Rigidbody2D rb;

    public override void OnEnter(ZombieController zombie)
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = zombie.GetComponent<Rigidbody2D>();

        ChangeTarget(player);
    }
    public override void OnTriggerEnter2D(ZombieController zombie, Collider2D collider)
    {
        if (collider.tag == "Player") // Or any deployable units
        {
            zombie.enemyInArea += 1;
        }
    }

    public override void OnTriggerExit2D(ZombieController zombie, Collider2D collider)
    {
        if (collider.tag == "Player") // Or any deployable units
        {
            zombie.enemyInArea -= 1;
        }
    }

    void ChangeTarget(Transform _target)
    {
        target = _target;
    }

    void ChaseTarget(ZombieController zombie)
    {
        if (target != null)
        {
            float distanceToTarget = target.position.x - zombie.transform.position.x;
            Vector3 direction = distanceToTarget < 0 ? Vector3.left : Vector3.right;
            zombie.transform.localScale = direction.x < 0 ? new Vector3(5, 5, 1) : new Vector3(-5, 5, 1);

            rb.velocity = direction * zombie.speed;
        }
    }

    public override void Update(ZombieController zombie)
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

    bool OnAttackRange(ZombieController zombie){
        Collider2D hitEnemy = Physics2D.OverlapCircle(zombie.attackPosition.position, 0.5f * zombie.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            return hitEnemy.gameObject == target.gameObject;
        } else {
            return false;
        }
        
    }
}
