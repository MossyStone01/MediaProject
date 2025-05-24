using System;
using Tomino;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private JumpStageMgr jumpStageMgr;
    private float moveSpeed = 6f;  // 장애물 이동 속도

    private void Start()
    {
        moveSpeed += GameMgr.Instance.TimeFactor;
        jumpStageMgr = GetComponentInParent<JumpStageMgr>();
    }

    void Update()
    {
        // 왼쪽 방향으로 이동 (X축 음의 방향)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumpStageMgr.ShowResultAndProceed(false);
        }
    }
}