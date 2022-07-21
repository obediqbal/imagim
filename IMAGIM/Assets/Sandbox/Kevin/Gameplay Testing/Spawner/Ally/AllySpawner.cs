using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnedAlly
{
    public GameObject allyPrefab;
    public float delayToNextSpawn;
}

[System.Serializable]
public class AllySpawner
{
    public Transform spawnPoint;
    public bool spawnInfinitely;
    public bool spawnAtConstantTime;
    public float timeBetweenSpawn;
    public float delayToFirstSpawn;
    public SpawnedAlly[] allySpawned;
}

