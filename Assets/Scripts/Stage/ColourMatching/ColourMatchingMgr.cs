using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class ColourMatchingMgr : MonoBehaviour
{
    public TMP_Text colorText;           // 문제 텍스트
    public Button[] colorButtons;        // 버튼들 (빨강, 초록, 파랑 등)
    public TMP_Text resultText;          // 결과 텍스트

    private Color[] colors = { Color.red, Color.green, Color.blue };
    private string[] colorNames = { "Red", "Green", "Blue" };
    
    private int correctColorIndex;
    private bool gameActive = false;

    private void Start()
    {
        resultText.text = "";
        StartCoroutine(SetupGame());

        // 각 버튼에 클릭 이벤트 연결
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            colorButtons[i].onClick.AddListener(() => OnColorButtonClicked(index));
        }
    }

    IEnumerator SetupGame()
    {
        yield return new WaitForSeconds(1f); // 준비 시간
        
        int wordIndex = Random.Range(0, colorNames.Length);
        correctColorIndex = Random.Range(0, colors.Length);

        colorText.text = colorNames[wordIndex];               // 예: "Red"
        colorText.color = colors[correctColorIndex];          // 예: 실제 텍스트 색은 파란색

        gameActive = true;
    }

    private void Update()
    {
        if (!gameActive)
            return;

        if (GameMgr.Instance.UpdateTimer())
        {
            ShowResultAndProceed(false);
        }
    }

    public void OnColorButtonClicked(int selectedIndex)
    {
        if (!gameActive) return;

        gameActive = false;

        bool isCorrect = selectedIndex == correctColorIndex;

        ShowResultAndProceed(isCorrect);
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
