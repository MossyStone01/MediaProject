using UnityEngine;

public class MarioPlayer : MonoBehaviour
{
    public float jumpForce = 7f;               // 점프 힘
    private Collider2D Collider;

    private Rigidbody2D rb;
    private bool isGrounded;

    private float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        
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
}
