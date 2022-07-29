using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/Zombie")]
public class ZombieUnit : Unit
{
    ZombieStateManager currentState;
    public Transform lockedEnemy;
    public float speed, attackRange, attackDelay;
    public List<Transform> enemiesInRange = new List<Transform>();
    [HideInInspector] public int enemyInArea = 0;
    [HideInInspector] public float xOrientation;
    public Transform attackPosition;

    public ZombieStateTower TargetTower = new ZombieStateTower();
    public ZombieStateChase TargetClosest = new ZombieStateChase();
    public ZombieStateAttack Attacking = new ZombieStateAttack();

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
    }

    public void SwitchState(ZombieStateManager state)
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


