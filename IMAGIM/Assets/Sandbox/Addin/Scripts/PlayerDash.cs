using UnityEngine;
using System.Collections;
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashDuration = 2f;
    [HideInInspector] public int dashCharge = 1;
    private Rigidbody2D rb;
    private float gravityScale;
    private Player playerMovement;
    private Animator animator;
    [HideInInspector] public bool inDash = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        playerMovement = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash") && dashCharge > 0 && !inDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash() {
        dashCharge = 0;
        inDash = true;
        animator.SetTrigger("Dash");
        rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dashSpeed, 0);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = gravityScale;
        inDash = false;
    }
}
