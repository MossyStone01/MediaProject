using System;
using UnityEngine;

public class SimpleHorizontalController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float moveInput;
    public float MoveSpeed = 5f;
    
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 좌우 입력 받기 (좌: -1, 우: 1)
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Rigidbody2D에 물리적 힘 적용 (속도 조정 방식)
        rigidbody2D.linearVelocity = new Vector2(moveInput * MoveSpeed, rigidbody2D.linearVelocity.y);
    }
}
