using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Your score : " + GameMgr.Instance.score.ToString();
    }
}
