using UnityEngine;

public class BlockPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float moveStep = 1f;   // 한 번에 이동할 거리
    public float yLimit = 4f;     // 이동 제한

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            TryMove(moveStep);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            TryMove(-moveStep);
        }
    }
    
    void TryMove(float step)
    {
        float newY = Mathf.Clamp(transform.position.y + step, -yLimit, yLimit);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
