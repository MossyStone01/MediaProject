using System;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
