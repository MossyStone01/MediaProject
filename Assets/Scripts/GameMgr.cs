using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }
    
    [HideInInspector] public int life = 5;
    [HideInInspector] public string playerName;
    [HideInInspector] public int score = 0;
    private int index;
    private string rankingSceneName = "RankingScene";
    private string startSceneName = "StartScene";
    
    private AudioSource audioSource;
    public string[] sceneNames;
    
    public float timerDuration = 10f;
    [HideInInspector] public float timer;
    private bool gameStart = false;

    public float TimeFactor = 0.0f; // 스테이지가 지날 수록 점점 빨라지는 구조로 만들기 위한 factor

    public void EnterEnrollScene()
    {
        SceneManager.LoadScene("EnrollScene");
    }
    
    public void StartGame()
    {
        if (sceneNames.Length == 0)
        {
            Debug.LogWarning("씬 목록이 비어 있습니다.");
            return;
        }

        index = 1;
        string sceneToLoad = sceneNames[index];
        Debug.Log("로딩할 랜덤 씬: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
        gameStart = true;
    }

    public void Reset()
    {
        life = 5;
        score = 0;
        timer = timerDuration;
        EnterEnrollScene();
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();
        timer = timerDuration;
    }

    public bool UpdateTimer() // 각 Stage Manager에서 호출해서 Timer 업데이트.
    {
        if(gameStart)
            timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OptimiseTimer()
    {
        timer = timerDuration;
        timer -= TimeFactor; // 시간이 지나면서 난이도 Up
    }

    public void EndStage(bool clear)
    {
        gameStart = false;
        if (clear)
        {
            // Stage Clear 시

            score += 1;
            Debug.Log("Clear!");
        }
        else
        {
            {
                // Stage Clear Fail

                life -= 1;
                Debug.Log("Fail!");
                if (life <= 0)
                {
                    string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
                    
                    string key = "Ranking_" + System.DateTime.Now.Ticks;
                    string value = playerName + " : " + score;
                    PlayerPrefs.SetString(key, value);

                    // 랭킹 키 목록 갱신
                    string keys = PlayerPrefs.GetString("Ranking_Keys", "");
                    keys += key + ";";
                    PlayerPrefs.SetString("Ranking_Keys", keys);

                    Debug.Log("랭킹 등록 완료: " + value);

                    
                    string sceneToLoad = sceneNames[0]; // End Game Scene 인덱스 0으로 유지
                    SceneManager.LoadScene(sceneToLoad);
                    return;
                }
            }
        }
        ChangeScene();
        
        timer = timerDuration;
        TimeFactor += 0.1f;
        gameStart = true;
    }

    private void ChangeScene()
    {
        if (sceneNames.Length <= index + 1)
        {
            index = 1;
        }
        else
        {
            index += 1;
        }
        
        string sceneToLoad = sceneNames[index];
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void OnSubmitName(string submittedName)
    {
        playerName = submittedName;
        PlayerPrefs.SetString("PlayerName", submittedName);
        Debug.Log("이름 저장 완료: " + submittedName);
    }

    public void EnterRankingScene()
    {
        SceneManager.LoadScene(rankingSceneName);
    }
    
    public void RestartGame()
    {
        Reset();
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene(startSceneName);
    }
}
