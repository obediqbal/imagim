using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/Warrior")]
public class WarriorUnit : Unit
{
        // State Machine Related
    WarriorStateManager currentState;
    public WarriorStateTower TargetTower = new WarriorStateTower();
    public WarriorStateChase TargetClosest = new WarriorStateChase();
    public WarriorStateAttack Attacking = new WarriorStateAttack();

    // Movement and Attack
    public Transform lockedEnemy;
    public float speed;
    public float attackRange, attackDelay;
    public Transform attackPosition;
    
    // Chase Mechanism
    public List<Transform> enemiesInChaseRange = new List<Transform>();
    [SerializeField] private Transform chaseRangeCenter;
    [SerializeField] private Vector2 chaseRangeSize;
    public LayerMask whatIsEnemy; // What this warrior will chase

    // Components Reference
    [SerializeField] Animator animator;
    private float move;
    [HideInInspector] public Rigidbody2D rb;
    private Vector3 initialScale;
    
    private void Start()
    {
        currentState = TargetTower;
        currentState.OnEnter(this);

        SetMaxHealth(100);
        rb = GetComponent<Rigidbody2D>();

        initialScale = transform.localScale;
    }

    private void Update()
    {
        currentState.Update(this);
        currentState.HandleStateSwitching(this);

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

        AnimationHandler();
        CheckEnemiesinChaseRange();
    }

    public void SwitchState(WarriorStateManager state)
    {
        currentState = state;
        currentState.OnEnter(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);

        Gizmos.color = enemiesInChaseRange.Count > 0 ? Color.red : Color.green;
        Gizmos.DrawWireCube(chaseRangeCenter.position, chaseRangeSize);
    }

    // Outside State Machine
    public void ChaseTarget(Transform target)
    {
        // Moving towards target
        float directionSign = Mathf.Sign(transform.position.x - target.position.x);
        Vector3 direction = Vector3.left * directionSign;
        rb.velocity = direction * speed;

        // Changing Warrior Orientation
        Vector3 scale = transform.localScale;
        scale.x = directionSign * initialScale.x;
        transform.localScale = scale;
    }

    public void CheckEnemiesinChaseRange()
    {
        enemiesInChaseRange.Clear();
        RaycastHit2D[] hitEnemies = Physics2D.BoxCastAll(chaseRangeCenter.position, chaseRangeSize, 0, Vector2.zero, 0, whatIsEnemy); 
        foreach (RaycastHit2D enemy in hitEnemies) 
        {
            enemiesInChaseRange.Add(enemy.transform);
        }
    }

    public void LockClosest()
    {
        if (enemiesInChaseRange.Count > 0)
        {
            Transform closest = enemiesInChaseRange[0];
            foreach (Transform enemy in enemiesInChaseRange)
            {
                if (Vector3.Distance(transform.position, enemy.position) < Vector3.Distance(transform.position, closest.position))
                {
                    closest = enemy;
                }
            }
            lockedEnemy = closest;
        }
    }

    public bool TargetOnAttackRange()
    {
        RaycastHit2D hitEnemy = Physics2D.CircleCast(attackPosition.position, 0.7f * attackRange, Vector2.zero, 0f, whatIsEnemy);        
        return GameObject.ReferenceEquals(hitEnemy.transform, lockedEnemy);
    }
    
    private void AnimationHandler()
    {
        move = rb.velocity.x;
        if (move == 0) {
            animator.SetBool("isWalking", false);
        }
        else {
            animator.SetBool("isWalking", true);
        }
    }
}


