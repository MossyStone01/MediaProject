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
    
    private AudioSource audioSource;
    public string[] sceneNames;
    
    public float timerDuration = 5f;
    [HideInInspector] public float timer;
    private bool gameStart = false;

    private float times = 1.0f; // 스테이지가 지날 수록 점점 빨라지는 구조로 만들기 위한 factor

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

        index = Random.Range(1, sceneNames.Length);
        string sceneToLoad = sceneNames[index];
        Debug.Log("로딩할 랜덤 씬: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
        gameStart = true;
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
    
    public void Update()
    {
        if(gameStart)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            
            EndStage(false); // 시간 제한 초과
        }
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
                    string value = playerName + ":" + score;
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
        gameStart = true;
    }

    private void ChangeScene()
    {
        int temp = Random.Range(1, sceneNames.Length);
        while (temp == index)
        {
            temp = Random.Range(1, sceneNames.Length);
        }
        
        index = temp;
        string sceneToLoad = sceneNames[index];
        Debug.Log("로딩할 랜덤 씬: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void OnSubmitName(string submittedName)
    {
        playerName = submittedName;
        PlayerPrefs.SetString("PlayerName", submittedName);
        Debug.Log("이름 저장 완료: " + submittedName);

        // 이름 저장 후 게임 시작으로 넘어가기
        // SceneManager.LoadScene("GameScene"); 등
    }

    public void EnterRankingScene()
    {
        SceneManager.LoadScene(rankingSceneName);
    }
}
