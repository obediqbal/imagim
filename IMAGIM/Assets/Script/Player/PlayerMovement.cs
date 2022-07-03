using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D box;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpStrength = 1f;
    [SerializeField] bool Opposite;
    private Vector2 move;
    [SerializeField] Animator animator;
    SpriteRenderer sprite;
    float movex, movey;
    [SerializeField] private LayerMask platformlayer;
    public float grounddistance = 3f;
    private float currentGap;
    Vector3 lastpos;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxisRaw("Horizontal");
        if (Opposite)
        {
            // gerak
            if (isMentokKanan() && movex < 0 || isMentokKiri() && movex > 0) movex = 0;
            transform.position = transform.position + new Vector3(-1 * movex * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetFloat("Horizontal", movex);
        }
        else
        {
            // gerak
            if (isMentokKanan() && movex > 0 || isMentokKiri() && movex < 0) movex = 0;
            transform.position = transform.position + new Vector3(movex * moveSpeed * Time.deltaTime, 0, 0);
            animator.SetFloat("Horizontal", movex);
        }
        if (movex == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        // Jump
        if (isGrounded())
        {
            movey = 0;
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
                Debug.Log("Jump");
            }
        }
        else
        {
            Vector3 offset = transform.position - lastpos;
            if (offset.y < 0)
            {
                movey = -1;
                lastpos = transform.position;
            }
            else if (offset.y > 0)
            {
                movey = 1;
                lastpos = transform.position;
            }
        }
        move = new Vector2(movex * moveSpeed, movey);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    // Ground Check
    bool isGrounded()
    {
        RaycastHit2D boxcast = Physics2D.BoxCast(box.bounds.center, new Vector2(0.45f * box.bounds.size.x, 0.45f * box.bounds.size.y), 0f, Vector2.down, grounddistance, platformlayer);
        Debug.Log(boxcast.collider);
        return boxcast.collider != null;
    }

    bool isMentokKanan()
    {
        RaycastHit2D boxcast = Physics2D.BoxCast(box.bounds.center, new Vector2(0.45f * box.bounds.size.x, box.bounds.size.y), 0f, Vector2.right, grounddistance, platformlayer);
        Debug.Log(boxcast.collider);
        return boxcast.collider != null;
    }

    bool isMentokKiri()
    {
        RaycastHit2D boxcast = Physics2D.BoxCast(box.bounds.center, new Vector2(0.45f * box.bounds.size.x, box.bounds.size.y), 0f, Vector2.left, grounddistance, platformlayer);
        Debug.Log(boxcast.collider);
        return boxcast.collider != null;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector2(0.45f * box.bounds.size.x, box.bounds.size.y));
    //}
}
