using System;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    public GameObject projectilePrefab;   // 총알 프리팹
    public Transform firePoint;           // 총구 위치 (노즐)
    public float projectileSpeed = 50f;   // 총알 속도
    public float fireRate = 0.3f;         // 발사 간격 (초)
    
    private float nextFireTime = 1f;
    
    public float moveStep = 5f;   // 한 번에 이동할 거리
    public float xLimit = 5f;     // 이동 제한

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            TryMove(-moveStep);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TryMove(moveStep);
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * projectileSpeed; // 총구의 방향 기준 발사

        // 필요시: 일정 시간 후 자동 파괴
        Destroy(projectile, 5f);
    }
    
    void TryMove(float step)
    {
        float newX = Mathf.Clamp(transform.position.x + step, -xLimit, xLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            GameMgr.Instance.EndStage(false);
    }
}