using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Attack")]
public class ZombieStateAttack : ZombieStateManager
{
    Rigidbody2D rb;
    Animator animator;
    bool isAttacking = false;
    
    public override void OnEnter(ZombieUnit zombie)
    {
        rb = zombie.GetComponent<Rigidbody2D>();
        animator = zombie.GetComponent<Animator>();
        zombie.StartCoroutine(Attack(zombie));
    }

    IEnumerator Attack(ZombieUnit zombie)
    {
        animator.SetBool("inCombat", true);
        isAttacking = true;
        yield return new WaitForSeconds(zombie.attackDelay);
        Collider2D hitEnemy = Physics2D.OverlapCircle(zombie.attackPosition.position, zombie.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            Debug.Log("Hit enemy : " + hitEnemy.name);
            IDamagable damagable = hitEnemy.GetComponent<IDamagable>();
            if (damagable != null)
            {
                Debug.Log("Hit enemy : " + hitEnemy.name);
                damagable.TakeDamage(20);
            }
        }
        else
        {
            Debug.Log("Attack Missed");
        }

        isAttacking = false;
        animator.SetBool("inCombat", false);
        zombie.SwitchState(zombie.TargetClosest);
    }

    public override void Update(ZombieUnit zombie)
    {
        rb.velocity = Vector2.zero;
    }
    public override void OnTriggerEnter2D(ZombieUnit zombie, Collider2D collider)
    {
        if (zombie.enemyInArea == 0 && !isAttacking)
        {
            zombie.SwitchState(zombie.TargetTower);
        }
    }

    public override void OnTriggerExit2D(ZombieUnit zombie, Collider2D collider)
    {
        zombie.enemyInArea -= 1;
    }
}
