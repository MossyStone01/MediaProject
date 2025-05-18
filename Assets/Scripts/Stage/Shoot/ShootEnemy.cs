using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private CircleCollider2D collider;
    public float moveSpeed = 4f; // 아래로 이동하는 속도

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            gameObject.SetActive(false); // Enemy 삭제
        }
        else if (other.CompareTag("Player"))
        {
            GameMgr.Instance.EndStage(false);
        }
    }
}
