using UnityEngine;

public class AvoidEnemy : MonoBehaviour
{
    private CircleCollider2D collider;
    private AvoidMgr AvoidMgr;
    public float moveSpeed = 4f; // 아래로 이동하는 속도

    private void Start()
    {
        AvoidMgr = GetComponentInParent<AvoidMgr>();
        collider = GetComponent<CircleCollider2D>();
        moveSpeed += GameMgr.Instance.TimeFactor;
    }

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AvoidMgr.ShowResultAndProceed(false);
        }
    }
}
