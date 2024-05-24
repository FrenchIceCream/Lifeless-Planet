using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnRate = 1f;

    float currentTime = 0f;

    void Update()
    {
        if (currentTime > spawnRate)
        {
            currentTime = 0;
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity);
        }
        currentTime += Time.deltaTime;
    }
}
