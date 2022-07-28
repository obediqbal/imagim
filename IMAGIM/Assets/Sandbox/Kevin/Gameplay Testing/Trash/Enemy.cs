using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] Animator animator;
    [SerializeField] CircleCollider2D combatRange;
    private float move;
    private Vector2 playerPosition;
    public bool isChasing = true;

    private void Start()
    {
        playerPosition = player.transform.position;
        combatRange = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (isChasing)
        {
            animator.SetBool("inCombat", false);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
            animator.SetFloat("Horizontal", move);
        }
        else if (!isChasing)
        {
            animator.SetBool("inCombat", true);

        }
        if (move == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
        }
        else
        {
            isChasing = true;
        }
    }
}
