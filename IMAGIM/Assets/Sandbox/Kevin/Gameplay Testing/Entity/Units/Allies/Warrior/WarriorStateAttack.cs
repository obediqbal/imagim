using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Attack")]
public class WarriorStateAttack : WarriorStateManager
{
    Rigidbody2D rb;
    Animator animator;
    bool isAttacking = false;
    
    public override void OnEnter(WarriorUnit warrior)
    {
        rb = warrior.GetComponent<Rigidbody2D>();
        animator = warrior.GetComponent<Animator>();
        warrior.StartCoroutine(Attack(warrior));
    }

    IEnumerator Attack(WarriorUnit warrior)
    {
        animator.SetBool("inCombat", true);
        isAttacking = true;
        yield return new WaitForSeconds(warrior.attackDelay);
        Collider2D hitEnemy = Physics2D.OverlapCircle(warrior.attackPosition.position, warrior.attackRange, LayerMask.GetMask("Ally"));
        IDamagable damagable = hitEnemy.GetComponent<IDamagable>();
        if (hitEnemy != null && damagable != null)
        {
            Debug.Log("Hit enemy : " + hitEnemy.name);
            damagable.TakeDamage(20);
        } 
        else
        {
            Debug.Log("Attack Missed");
        }
        isAttacking = false;
        animator.SetBool("inCombat", false);
        warrior.SwitchState(warrior.TargetClosest);
    }

    public override void Update(WarriorUnit warrior)
    {
        rb.velocity = Vector2.zero;
    }
    public override void OnTriggerEnter2D(WarriorUnit warrior, Collider2D collider)
    {
        if (warrior.enemyInArea == 0 && !isAttacking)
        {
            warrior.SwitchState(warrior.TargetTower);
        }
    }

    public override void OnTriggerExit2D(WarriorUnit warrior, Collider2D collider)
    {
        warrior.enemyInArea -= 1;
    }
}
