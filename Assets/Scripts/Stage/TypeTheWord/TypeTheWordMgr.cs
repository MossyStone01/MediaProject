using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public class TypeTheWordMgr : MonoBehaviour
{
    public TMP_Text targetWordText;
    public TMP_InputField inputField;
    public TMP_Text resultText;

    private string targetWord;
    private bool gameActive = false;

    private string[] wordPool = { "apple", "unity", "score", "quick", "dream", "piano", "robot", "green" };

    void Start()
    {
        StartCoroutine(StartMiniGame());
    }

    IEnumerator StartMiniGame()
    {
        yield return new WaitForSeconds(1f); // 준비 시간
        targetWord = wordPool[Random.Range(0, wordPool.Length)];
        targetWordText.text = "Type: " + targetWord;
        inputField.text = "";
        inputField.ActivateInputField();
        gameActive = true;
        inputField.onSubmit.AddListener(CheckAnswer);   
    }

    private void Update()
    {
        if (!gameActive)
            return;

        if (GameMgr.Instance.UpdateTimer()) // 시간제한 초과
        {
            ShowResultAndProceed(false);
        }
    }

    private void CheckAnswer(string input)
    {
        if (input.Trim().ToLower() == targetWord.ToLower())
        {
            ShowResultAndProceed(true);
        }
        else
        {
            ShowResultAndProceed(false);
        }
    }

    public void ShowResultAndProceed(bool isGameCleared)
    {
        gameActive = false;
        StartCoroutine(ShowResultCoroutine(isGameCleared));
    }
    
    private IEnumerator ShowResultCoroutine(bool isGameCleared)
    {
        gameActive = false;
        
        if (isGameCleared)
        {
            resultText.text = "<color=blue>Success!</color>";
            yield return new WaitForSeconds(2f); // 2초 대기
            
            GameMgr.Instance.EndStage(true);
        }
        else
        {
            resultText.text = "<color=red>Failed!</color>";
            yield return new WaitForSeconds(2f); // 2초 대기
            
            GameMgr.Instance.EndStage(false);
        }
    }
}
