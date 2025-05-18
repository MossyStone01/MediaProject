using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 9f;  // 장애물 이동 속도

    void Update()
    {
        // 왼쪽 방향으로 이동 (X축 음의 방향)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}