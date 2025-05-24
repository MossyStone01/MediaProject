using TMPro;
using System.Collections;
using UnityEngine;

public class PressSpaceMgr : MonoBehaviour
{
    public TMP_Text remainingText;
    public TMP_Text resultText;

    private int targetCount = 24;
    private int currentCount = 0;

    private bool gameActive = false;

    void Start()
    {
        targetCount = Mathf.RoundToInt(targetCount + GameMgr.Instance.TimeFactor);
        currentCount = 0;

        gameActive = true;
    }

    void Update()
    {
        if (!gameActive) return;

        if (GameMgr.Instance.UpdateTimer())
        {
            ShowResultAndProceed(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentCount++;
            remainingText.text = "Remaining : " + (targetCount - currentCount);

            if (currentCount >= targetCount)
            {
                ShowResultAndProceed(true);
            }
        }

        
    }

    private void ShowResultAndProceed(bool isGameCleared)
    {
        gameActive = false;
        StartCoroutine(ShowResultCoroutine(isGameCleared));
    }

    private IEnumerator ShowResultCoroutine(bool isGameCleared)
    {
        if (isGameCleared)
        {
            resultText.text = "<color=blue>Success!</color>";
            yield return new WaitForSeconds(2f);
            GameMgr.Instance.EndStage(true);
        }
        else
        {
            resultText.text = "<color=red>Failed!</color>";
            yield return new WaitForSeconds(2f);
            GameMgr.Instance.EndStage(false);
        }
    }
}
