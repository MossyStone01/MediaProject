using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EndSceneMgr : MonoBehaviour
{
    public TMP_Text rankingText;

    void Start()
    {
        List<string> rankings = new List<string>();

        foreach (var key in PlayerPrefsKeys())
        {
            if (key.StartsWith("Ranking_"))
            {
                rankings.Add(PlayerPrefs.GetString(key));
            }
        }

        rankings.Sort((a, b) => {
            int scoreA = int.Parse(a.Split(':')[1]);
            int scoreB = int.Parse(b.Split(':')[1]);
            return scoreB.CompareTo(scoreA); // 높은 점수 순 정렬
        });

        rankingText.text = string.Join("\n", rankings);
    }

    IEnumerable<string> PlayerPrefsKeys()
    {
#if UNITY_EDITOR
        var prefs = UnityEditor.EditorPrefs.GetString("UnityEditor.PlayerSettings.kPlayerPrefs");
        foreach (var key in PlayerPrefs.GetString("UnityEditor.PlayerSettings.kPlayerPrefs").Split(','))
        {
            yield return key.Trim();
        }
#else
        // 런타임에서는 PlayerPrefs의 키 전체 목록을 직접 가져올 수 없음 (제한 있음)
        yield break;
#endif
    }
}
