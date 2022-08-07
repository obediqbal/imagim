using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Attack")]
public class ZombieStateAttack : ZombieStateManager
{
    Animator animator;
    bool isAttacking = false;
    
    public override void OnEnter(ZombieUnit zombie)
    {
        animator = zombie.GetComponent<Animator>();
        zombie.StartCoroutine(Attack(zombie));
    }

    IEnumerator Attack(ZombieUnit zombie)
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        yield return new WaitForSeconds(zombie.attackDelay);
        RaycastHit2D hitEnemy = Physics2D.CircleCast(zombie.attackPosition.position, zombie.attackRange, Vector2.zero, 0f, zombie.whatIsEnemy);
        if (hitEnemy)
        {
            IDamagable damagable = hitEnemy.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(20);
            }
        }

        isAttacking = false;
        zombie.SwitchState(zombie.TargetTower);
    }

    public override void Update(ZombieUnit zombie)
    {
        zombie.rb.velocity = Vector2.zero;
    }

    public override void HandleStateSwitching(ZombieUnit zombie) {}
}
