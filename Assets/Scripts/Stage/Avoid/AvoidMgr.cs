using System.Collections;
using TMPro;
using UnityEngine;

public class AvoidMgr : MonoBehaviour
{
    public float moveStep = 5f;   // 한 번에 이동할 거리
    public float xLimit = 5f;     // 이동 제한
    [SerializeField] private TMP_Text resultText;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            TryMove(-moveStep);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TryMove(moveStep);
        }

        if (GameMgr.Instance.UpdateTimer())
        {
            GameMgr.Instance.EndStage(true);
        }

    }
    
    void TryMove(float step)
    {
        float newX = Mathf.Clamp(transform.position.x + step, -xLimit, xLimit);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    
    public void ShowResultAndProceed(bool isGameCleared)
    {
        StartCoroutine(ShowResultCoroutine(isGameCleared));
    }
    
    private IEnumerator ShowResultCoroutine(bool isGameCleared)
    {
        if (isGameCleared)
        {
            resultText.text = "<color=blue>Success!</color>";
            Time.timeScale = 0f; // 게임 시간 멈춤
            yield return new WaitForSecondsRealtime(2f); // 실제 시간 기준으로 2초 대기
            Time.timeScale = 1f; // 게임 시간 재개

            
            GameMgr.Instance.EndStage(true);
        }
        else
        {
            resultText.text = "<color=red>Failed!</color>";
            Time.timeScale = 0f; // 게임 시간 멈춤
            yield return new WaitForSecondsRealtime(2f); // 실제 시간 기준으로 2초 대기
            Time.timeScale = 1f; // 게임 시간 재개

            
            GameMgr.Instance.EndStage(false);
        }
    }
}
