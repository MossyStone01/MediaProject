using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private Transform[] spawnPoints;
    [SerializeField]private GameObject enemyPrefab;           // 생성할 적 프리팹
    private float spawnInterval = 1f;         // 생성 간격 (초)

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            SpawnEnemyAtRandomPoint();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemyAtRandomPoint()
    {
        if (spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Instantiate(enemyPrefab, spawnPoint);
    }
}
