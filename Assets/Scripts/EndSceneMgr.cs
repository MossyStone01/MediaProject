using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class EndSceneMgr : MonoBehaviour
{
    public TMP_Text rankingText;

    private void Start()
    {
        rankingText.text = "Your score : " + GameMgr.Instance.score;
    }

    public void PressRestart()
    {
        GameMgr.Instance.RestartGame();   
    }

    public void PressQuit()
    {
        GameMgr.Instance.QuitGame();
    }
}
