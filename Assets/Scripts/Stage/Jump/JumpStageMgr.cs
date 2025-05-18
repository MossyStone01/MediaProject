using System;
using UnityEngine;

public class JumpStageMgr : MonoBehaviour
{
    private BoxCollider2D collider2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameMgr.Instance.EndStage(true);
        }
    }
}
