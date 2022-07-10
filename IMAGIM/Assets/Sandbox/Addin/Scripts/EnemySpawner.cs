using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnedEnemy{
    public GameObject enemyPrefab;
    public float delayToNextSpawn;
}

[System.Serializable]
public class EnemySpawner
{
    public Transform spawnPoint;
    public bool spawnInfinitely;
    public bool spawnAtConstantTime;
    public float timeBetweenSpawn;
    public float delayToFirstSpawn;
    public SpawnedEnemy[] enemySpawned;
}

