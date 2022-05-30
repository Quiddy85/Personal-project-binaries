using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject PowerupPrefab;
    private float spawnRange = 23;
    private float invokeDelay = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke methods
        InvokeRepeating("SpawnEnemies", 5, invokeDelay / 3);
        InvokeRepeating("SpawnCoins", 1, invokeDelay / 5);
        InvokeRepeating("SpawnPowerups", 25, invokeDelay * 5);
    }

    // Spawn objects
    private void SpawnEnemies()
    {
        Instantiate(enemyPrefab, SpawnPosition(), enemyPrefab.transform.rotation);
    }

    private void SpawnCoins()
    {
        Instantiate(coinPrefab, SpawnPosition(), coinPrefab.transform.rotation);
    }

    private void SpawnPowerups()
    {
        Instantiate(PowerupPrefab, SpawnPosition(), PowerupPrefab.transform.rotation);
    }

    // Set random spawn locations
    private Vector3 SpawnPosition()
    {
        float rangeX = Random.Range(-spawnRange, spawnRange);
        float rangeZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPosition = new Vector3(rangeX, 1, rangeZ);
        return spawnPosition;
    }
}
