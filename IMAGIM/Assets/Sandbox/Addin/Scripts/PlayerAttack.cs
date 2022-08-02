using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private float attackAnimationDelay = 0.07f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    private bool canAttack = true;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1") && canAttack) {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() {
        canAttack = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimationDelay);
        DoAttack();
        float remainingAttackDuration = attackCooldown - attackAnimationDelay;
        yield return new WaitForSeconds(remainingAttackDuration);
        canAttack = true;
    }

    private void DoAttack() {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        if (enemiesToDamage.Length > 0) {
            foreach (Collider2D enemy in enemiesToDamage) {
                Debug.Log("We hit " + enemy.name);
            }
        } else {
            Debug.Log("We hit nothing, just like the meaning of our life");
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
