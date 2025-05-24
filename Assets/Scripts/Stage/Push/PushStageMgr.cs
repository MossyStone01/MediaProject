using System;
using UnityEngine;

public class PushStageMgr : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        
        GameMgr.Instance.OptimiseTimer();
    }

    private void Update()
    {
        if (GameMgr.Instance.UpdateTimer())
        {
            GameMgr.Instance.EndStage(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Finish"))
            GameMgr.Instance.EndStage(true);
        else if(other.CompareTag("Player"))
            GameMgr.Instance.EndStage(false);
    }
}
