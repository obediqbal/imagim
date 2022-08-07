using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Attack")]
public class WarriorStateAttack : WarriorStateManager
{
    Animator animator;
    bool isAttacking = false;
    
    public override void OnEnter(WarriorUnit warrior)
    {
        animator = warrior.GetComponent<Animator>();
        warrior.StartCoroutine(Attack(warrior));
    }

    IEnumerator Attack(WarriorUnit warrior)
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        yield return new WaitForSeconds(warrior.attackDelay);
        RaycastHit2D hitEnemy = Physics2D.CircleCast(warrior.attackPosition.position, warrior.attackRange, Vector2.zero, 0f, warrior.whatIsEnemy);
        if (hitEnemy)
        {
            IDamagable damagable = hitEnemy.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(20);
            }
        }

        isAttacking = false;
        warrior.SwitchState(warrior.TargetTower);
    }

    public override void Update(WarriorUnit warrior)
    {
        warrior.rb.velocity = Vector2.zero;
    }

    public override void HandleStateSwitching(WarriorUnit warrior) {}
}
