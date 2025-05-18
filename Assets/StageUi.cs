using UnityEngine;
using TMPro;

public class StageUi : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        float remainingTime = Mathf.Clamp(GameMgr.Instance.timer, 0f, GameMgr.Instance.timerDuration);
        timerText.text = $"Time: {remainingTime:F2}";
    }
}
