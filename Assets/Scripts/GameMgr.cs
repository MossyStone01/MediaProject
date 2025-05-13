using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }
    
    [HideInInspector] public int life = 5;
    [HideInInspector] public int score = 0;
    private int index;
    
    private AudioSource audioSource;
    public string[] sceneNames;
    
    public float timerDuration = 5f;
    private float timer;
    private bool gameStart = false;

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
        }
        else
        {
            {
                // Stage Clear Fail

                life -= 1;
                if (life <= 0)
                {
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
}
