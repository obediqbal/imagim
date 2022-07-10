using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttacking : ZombieBaseState
{
    Rigidbody2D rb;
    Animator animator;
    bool isAttacking = false;
    
    public override void OnEnter(ZombieController zombie)
    {
        rb = zombie.GetComponent<Rigidbody2D>();
        animator = zombie.GetComponent<Animator>();
        zombie.StartCoroutine(Attack(zombie));
    }

    IEnumerator Attack(ZombieController zombie)
    {
        animator.SetBool("inCombat", true);
        isAttacking = true;
        yield return new WaitForSeconds(zombie.attackDelay);
        Collider2D hitEnemy = Physics2D.OverlapCircle(zombie.attackPosition.position, zombie.attackRange, LayerMask.GetMask("Ally"));
        if (hitEnemy != null)
        {
            Debug.Log("Hit enemy : " + hitEnemy.name);
        } else {
            Debug.Log("Attack Missed");
        }

        isAttacking = false;
        animator.SetBool("inCombat", false);
        zombie.SwitchState(zombie.TargetClosest);
    }

    public override void Update(ZombieController zombie)
    {
        rb.velocity = Vector2.zero;
    }
    public override void OnTriggerEnter2D(ZombieController zombie, Collider2D collider)
    {
        if (zombie.enemyInArea == 0 && !isAttacking)
        {
            zombie.SwitchState(zombie.TargetTower);
        }
    }

    public override void OnTriggerExit2D(ZombieController zombie, Collider2D collider)
    {
        zombie.enemyInArea -= 1;
    }
}
