using System.Collections;
using UnityEngine;
using TMPro;

public class MathMaticalMgr : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField inputField;
    public TMP_Text resultText;

    private int correctAnswer;
    private bool gameActive = false;
    private float timeLimit = 5f;

    void Start()
    {
        StartCoroutine(StartMiniGame());
    }

    void Update()
    {
        if (!gameActive)
            return;

        if (GameMgr.Instance.UpdateTimer())
        {
            ShowResultAndProceed(false);
        }
    }

    IEnumerator StartMiniGame()
    {
        yield return new WaitForSeconds(1f); // 준비 시간

        GenerateQuestion();
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.onSubmit.AddListener(CheckAnswer);
        gameActive = true;
    }

    void GenerateQuestion()
    {
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);
        bool isAddition = Random.Range(0, 2) == 0;

        if (isAddition)
        {
            questionText.text = $"{a} + {b} = ?";
            correctAnswer = a + b;
        }
        else
        {
            questionText.text = $"{a} - {b} = ?";
            correctAnswer = a - b;
        }
    }
    void CheckAnswer(string input)
    {
        if (!gameActive) return;

        if (int.TryParse(input, out int userAnswer))
        {
            if (userAnswer == correctAnswer)
            {
                ShowResultAndProceed(true);
            }
            else
            {
                ShowResultAndProceed(false);
            }
        }
        else
        {
            ShowResultAndProceed(false);
        }
    }

    void ShowResultAndProceed(bool isCorrect)
    {
        gameActive = false;
        StartCoroutine(ShowResultCoroutine(isCorrect));
    }

    IEnumerator ShowResultCoroutine(bool isCorrect)
    {
        resultText.text = isCorrect ? "<color=blue>Success!</color>" : "<color=red>Failed!</color>";
        yield return new WaitForSeconds(2f);

        GameMgr.Instance.EndStage(isCorrect);
    }
}
