using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/Warrior")]
public class WarriorUnit : Unit
{
    WarriorStateManager currentState;
    public Transform lockedEnemy;
    public float speed, attackRange, attackDelay;
    public List<Transform> enemiesInRange = new List<Transform>();
    [HideInInspector] public int enemyInArea = 0;
    [HideInInspector] public float xOrientation;
    public Transform attackPosition;

    public WarriorStateTower TargetTower = new WarriorStateTower();
    public WarriorStateChase TargetClosest = new WarriorStateChase();
    public WarriorStateAttack Attacking = new WarriorStateAttack();

    [SerializeField] Animator animator;
    private float move;
    private Rigidbody2D rb;

    private void Start()
    {
        SetMaxHealth(100);
        rb = GetComponent<Rigidbody2D>();
        currentState = TargetTower;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        currentState.Update(this);
        move = rb.velocity.x;
        if (move == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SwitchState(WarriorStateManager state)
    {
        currentState = state;
        currentState.OnEnter(this);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        currentState.OnTriggerEnter2D(this, collider);
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        currentState.OnTriggerExit2D(this, collider);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    public void RetargetEnemy()
    {
        lockedEnemy = null;
    }
}


