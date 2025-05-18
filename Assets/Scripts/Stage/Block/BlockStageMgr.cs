using System;
using UnityEngine;

public class BlockStageMgr : MonoBehaviour
{
    public bool IsThisFront = false;
    public float MoveSpeed = 3.0f;
    
    void Update()
    {
        if(IsThisFront)
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameMgr.Instance.EndStage(true);
        } 
        else if (other.tag == "Enemy")
        {
            GameMgr.Instance.EndStage(false);
        }
    }
}
