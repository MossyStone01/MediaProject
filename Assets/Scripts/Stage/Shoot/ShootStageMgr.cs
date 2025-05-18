using System;
using UnityEngine;

public class ShootStageMgr : MonoBehaviour
{
    private BoxCollider2D collider;
    public bool IsThisUp = false;
    public float MoveSpeed = 4f; // 아래로 이동하는 속도

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsThisUp)
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") && !IsThisUp)
            GameMgr.Instance.EndStage(false);
        else if(other.CompareTag("Player") && IsThisUp)
            GameMgr.Instance.EndStage(true);
    }
}
