using System;
using UnityEngine;

public class JumpClearChecker : MonoBehaviour
{
    private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO : Claer 구현
        }
    }
}
