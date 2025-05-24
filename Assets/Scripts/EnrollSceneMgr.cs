using TMPro;
using Tomino;
using UnityEngine;

public class EnrollSceneMgr : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;

    public void NameSubmitted()
    {
        GameMgr.Instance.OnSubmitName(nameInputField.text);
        Debug.Log(nameInputField.text);
        GameMgr.Instance.StartGame();
    }
}
