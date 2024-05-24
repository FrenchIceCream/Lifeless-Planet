using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] pickupPrefabs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnRate = 1f;
    [SerializeField] float spawnRadius = 10f;

    float currentTime = 0f;

    void Update()
    {
        if (currentTime > spawnRate)
        {
            currentTime = 0;
            int prefabIndex = Random.Range(0, pickupPrefabs.Length);
            Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
            Instantiate(pickupPrefabs[prefabIndex], spawnPoint.position + new Vector3(spawnOffset.x, 0, spawnOffset.y), Quaternion.identity);
        }
        currentTime += Time.deltaTime;
    }
}
