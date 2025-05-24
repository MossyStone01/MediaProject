using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private ShootStageMgr shootStageMgr;
    public float moveSpeed = 4f; // 아래로 이동하는 속도

    private void Start()
    {
        moveSpeed += GameMgr.Instance.TimeFactor;
        shootStageMgr = GetComponentInParent<ShootStageMgr>();
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
            other.gameObject.SetActive(false); // Projectile 삭제
        }
        else if (other.CompareTag("Player"))
        {
            shootStageMgr.ShowResultAndProceed(false);
        }
    }
}
