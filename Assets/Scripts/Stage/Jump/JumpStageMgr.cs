using System.Collections;
using TMPro;
using UnityEngine;

public class JumpStageMgr : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    void Start()
    {

    }

    void Update()
    {
        if (GameMgr.Instance.UpdateTimer())
        {
            ShowResultAndProceed(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowResultAndProceed(false);
        }
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
