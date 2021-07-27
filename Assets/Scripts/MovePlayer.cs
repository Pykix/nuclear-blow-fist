using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    public bool isJumping;
    public bool isGrounding;

    private float horizontalMovement;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {

        if (Input.GetButtonDown("Jump") && isGrounding)
        {
            isJumping = true;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Flip(rb.velocity.x);
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        Move(horizontalMovement);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

    }

    void Move(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void Flip(float _velocity)
    {
        if (_velocity > .01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
