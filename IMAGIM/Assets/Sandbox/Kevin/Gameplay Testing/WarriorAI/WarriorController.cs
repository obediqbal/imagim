using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarriorController : MonoBehaviour
{
    WarriorBaseState currentState;
    public float speed, attackRange, attackDelay;
    [HideInInspector] public int enemyInArea = 0;
    [HideInInspector] public float xOrientation;
    public Transform attackPosition;

    public WarriorTargetTower TargetTower = new WarriorTargetTower();
    public WarriorTargetClosest TargetClosest = new WarriorTargetClosest();
    public WarriorAttacking Attacking = new WarriorAttacking();

    [SerializeField] Animator animator;
    private float move;
    private Rigidbody2D rb;

    private void Start()
    {
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

    public void SwitchState(WarriorBaseState state)
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
}


