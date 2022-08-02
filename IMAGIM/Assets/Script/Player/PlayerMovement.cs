using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float decceleration = 6f;
    [SerializeField] private float velPower = 0.9f;
    [HideInInspector] public float xInput; 
    private float xScale;
    [SerializeField] private float jumpSpeed = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    private PlayerDash playerDash;
    private bool inDash;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xScale = transform.localScale.x;
        boxCollider = GetComponent<BoxCollider2D>();
        playerDash = GetComponent<PlayerDash>();
    }

    private void Update() {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
        
        AnimationHandler(); 
        FlipPlayer();
        DashHandler();
    }

    private void FixedUpdate() {
        if (!inDash)
        {
            float targetSpeed = xInput * moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            
            rb.AddForce(movement * Vector2.right);
        }
    }

    private void FlipPlayer(){
        if (xInput != 0) {
            Vector3 scale = transform.localScale;
            scale.x = xScale * xInput;
            transform.localScale = scale;
        }
    }

    private void AnimationHandler(){
        if (xInput != 0 & rb.velocity.y == 0) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
    }

    public bool isGrounded(){
        Vector3 downChecker = new Vector3(0, -0.2f, 0);
        return Physics2D.OverlapBoxAll(boxCollider.bounds.center + downChecker, boxCollider.bounds.size, 0, LayerMask.GetMask("Ground")).Length > 0;
    }

    private void DashHandler(){
        if (isGrounded()) {
            playerDash.dashCharge = 1;
        }

        inDash = playerDash.inDash;
    }
}
