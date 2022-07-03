using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieController : MonoBehaviour
{
    ZombieBaseState currentState;
    public float speed, attackRange, attackDelay;
    [HideInInspector] public int enemyInArea = 0;
    public Transform attackPosition;
    public TextMeshProUGUI stateText;

    public ZombieTargetTower TargetTower = new ZombieTargetTower();
    public ZombieTargetClosest TargetClosest = new ZombieTargetClosest();
    public ZombieAttacking Attacking = new ZombieAttacking();

    [SerializeField] Animator animator;
    private bool isChasing = true;
    private float move;

    private void Start() {
        currentState = TargetTower;
        currentState.OnEnter(this);
    }

    private void Update() {
        currentState.Update(this);
        stateText.text = "Active State: " + currentState.GetType().Name;
        move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", move);
        if (move == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    public void SwitchState(ZombieBaseState state){
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
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}


