using System;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public float jumpForce = 100f;               // 점프 힘
    public Collider2D Collider;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            GameMgr.Instance.EndStage(false);
        }
    }
}