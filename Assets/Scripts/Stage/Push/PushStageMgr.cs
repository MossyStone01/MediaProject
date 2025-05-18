using System;
using UnityEngine;

public class PushStageMgr : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Finish"))
            GameMgr.Instance.EndStage(true);
        else if(other.CompareTag("Player"))
            GameMgr.Instance.EndStage(false);
    }
}
