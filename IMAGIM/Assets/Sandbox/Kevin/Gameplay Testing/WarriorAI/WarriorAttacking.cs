using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttacking : WarriorBaseState
{
    Rigidbody2D rb;
    Animator animator;
    bool isAttacking = false;

    public override void OnEnter(WarriorController warrior)
    {
        rb = warrior.GetComponent<Rigidbody2D>();
        animator = warrior.GetComponent<Animator>();
        warrior.StartCoroutine(Attack(warrior));
    }

    IEnumerator Attack(WarriorController warrior)
    {
        animator.SetBool("inCombat", true);
        isAttacking = true;
        yield return new WaitForSeconds(warrior.attackDelay);
        Collider2D hitEnemy = Physics2D.OverlapCircle(warrior.attackPosition.position, warrior.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            Debug.Log("Hit enemy : " + hitEnemy.name);
        }
        else
        {
            Debug.Log("Attack Missed");
        }

        isAttacking = false;
        animator.SetBool("inCombat", false);
        warrior.SwitchState(warrior.TargetClosest);
    }

    public override void Update(WarriorController warrior)
    {
        rb.velocity = Vector2.zero;
    }
    public override void OnTriggerEnter2D(WarriorController warrior, Collider2D collider)
    {
        if (warrior.enemyInArea == 0 && !isAttacking)
        {
            warrior.SwitchState(warrior.TargetTower);
        }
    }

    public override void OnTriggerExit2D(WarriorController warrior, Collider2D collider)
    {
        warrior.enemyInArea -= 1;
    }
}
