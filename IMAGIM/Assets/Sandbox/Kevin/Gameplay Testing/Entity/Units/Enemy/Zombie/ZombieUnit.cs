using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DKH.ResourceSystem;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/Zombie")]
public class ZombieUnit : Unit
{
    // State Machine Related
    ZombieStateManager currentState;
    public ZombieStateTower TargetTower = new ZombieStateTower();
    public ZombieStateChase TargetClosest = new ZombieStateChase();
    public ZombieStateAttack Attacking = new ZombieStateAttack();
    [SerializeField] Resource fota;
    // Movement and Attack
    public RaycastHit2D lockedEnemy;
    public float speed;
    public float attackRange, attackDelay;
    public Transform attackPosition;
    
    // Chase Mechanism
    public List<RaycastHit2D> enemiesInChaseRange = new List<RaycastHit2D>();
    [SerializeField] private Transform chaseRangeCenter;
    [SerializeField] private Vector2 chaseRangeSize;
    public LayerMask whatIsEnemy; // What this zombie will chase
    private Transform tower;

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
        tower = GameObject.FindGameObjectWithTag("AllyTower").transform;
        
        // Adapt enemy detection to only detect enemies in front (between this unit and the enemy tower)
        Vector3 initialChaseRangeCenter = chaseRangeCenter.localPosition; 
        float toTowerDirectionSign = Mathf.Sign(transform.position.x - tower.position.x);
        initialChaseRangeCenter.x = toTowerDirectionSign * chaseRangeCenter.localPosition.x;
        chaseRangeCenter.localPosition = initialChaseRangeCenter;
    }

    private void Update()
    {
        currentState.HandleStateSwitching(this);
        currentState.Update(this);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
            fota.SetValue(fota.GetValue() + 10);
        }

        AnimationHandler();
        CheckEnemiesinChaseRange();
    }

    public void SwitchState(ZombieStateManager state)
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

        // Changing Zombie Orientation
        Vector3 scale = transform.localScale;
        scale.x = directionSign * initialScale.x;

        chaseRangeCenter.parent = null;
        transform.localScale = scale;
        chaseRangeCenter.parent = transform;
    }

    public void CheckEnemiesinChaseRange()
    {
        RaycastHit2D[] enemies = Physics2D.BoxCastAll(chaseRangeCenter.position, chaseRangeSize, 0, Vector2.zero, 0, whatIsEnemy); 
        enemiesInChaseRange = new List<RaycastHit2D>(enemies);
    }

    public bool TargetOnAttackRange()
    {
        RaycastHit2D hitEnemy = Physics2D.CircleCast(attackPosition.position, 0.9f * attackRange, Vector2.zero, 0f, whatIsEnemy);
        if (hitEnemy && lockedEnemy)
        {
            return GameObject.ReferenceEquals(hitEnemy.transform.gameObject, lockedEnemy.transform.gameObject);
        }        
        return false;
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


