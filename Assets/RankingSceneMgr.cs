using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class RankingSceneMgr : MonoBehaviour
{
    public TMP_Text rankingText;

    void Start()
    {
        List<string> rankings = new List<string>();

        string keys = PlayerPrefs.GetString("Ranking_Keys", "");
        if (!string.IsNullOrEmpty(keys))
        {
            string[] keyArray = keys.Split(';');
            foreach (string key in keyArray)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    string value = PlayerPrefs.GetString(key);
                    if (!string.IsNullOrEmpty(value))
                    {
                        rankings.Add(value);
                    }
                }
            }

            rankings.Sort((a, b) =>
            {
                int scoreA = int.Parse(a.Split(':')[1]);
                int scoreB = int.Parse(b.Split(':')[1]);
                return scoreB.CompareTo(scoreA);
            });

            rankingText.text = string.Join("\n", rankings);
        }
        else
        {
            rankingText.text = "랭킹 데이터 없음";
        }
    }
}