using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public GameObject gameOver;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 1;
    //public vectorValue startingPosition;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource theme;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource death;
    [SerializeField] bool isFrozen;
    Vector2 move;
    [SerializeField] private LayerMask layerMask;

    void FixedUpdate()
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Debug.Log("Player spawn");
        theme.Play();
        Debug.Log("Now playing theme");
        isFrozen = false;
    }

    // Update is called once per frame
    private void Update()
    {
        var movement = Input.GetAxisRaw("Horizontal");
        if (!isFrozen)
        {
            animator.SetFloat("Horizontal", movement);
            if (movement == 0)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
            transform.position = transform.position + new Vector3(movement * moveSpeed * Time.deltaTime, 0, 0);
            if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(rb.velocity.y) < 1)
            {
                rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                Debug.Log("Jump");
                jump.Play();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Enemy")
        {
            isFrozen = true;
            theme.Stop();
            death.Play();
            gameOver.SetActive(true);
            Collider2D.Destroy(gameObject, 5);
            Debug.Log("Game Over");
        }
    }

}
