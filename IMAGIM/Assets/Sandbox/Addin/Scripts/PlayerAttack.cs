using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private float attackAnimationDelay = 0.07f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private float attackDamage = 20f;
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

    private void DoAttack() 
    {
        RaycastHit2D enemiesToDamage = Physics2D.CircleCast(attackPoint.position, attackRadius, Vector2.zero, 0, enemyLayers);
        if (enemiesToDamage) 
        {
            IDamagable damagable = enemiesToDamage.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(attackDamage);
            }
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
