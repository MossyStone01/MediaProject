using System;
using UnityEngine;

public class MarioStageMgr : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            GameMgr.Instance.EndStage(true);
    }
}


