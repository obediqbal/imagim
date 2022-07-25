using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnManager : MonoBehaviour
{
    [SerializeField] AllySpawner[] spawners;
    [SerializeField] Transform alliesParent;
    Coroutine spawnerCoroutine;
    private bool firstSpawn = true;

    void Start()
    {
        StartSpawning();
    }

    void StartSpawning(){
        foreach(AllySpawner spawner in spawners){
            spawnerCoroutine = StartCoroutine(AllySpawn(spawner));
        }
    }

    IEnumerator AllySpawn(AllySpawner spawner){
        if (firstSpawn)
        {
            yield return new WaitForSeconds(spawner.delayToFirstSpawn);
        }
        foreach (SpawnedAlly ally in spawner.allySpawned)
        {
            float delay = spawner.spawnAtConstantTime ? spawner.timeBetweenSpawn : ally.delayToNextSpawn;
            Instantiate(ally.allyPrefab, spawner.spawnPoint.position, spawner.spawnPoint.rotation, alliesParent);
            yield return new WaitForSeconds(delay);
        }

        firstSpawn = false;
        if (spawner.spawnInfinitely) {
            spawnerCoroutine = StartCoroutine(AllySpawn(spawner));
        }
    }
}
