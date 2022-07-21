using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemySpawner[] spawners;
    public Transform enemiesParent;
    Coroutine spawnerCoroutine;
    private bool firstSpawn = true;

    void Start()
    {
        StartSpawning();
    }

    void StartSpawning(){
        foreach(EnemySpawner spawner in spawners){
            spawnerCoroutine = StartCoroutine(EnemySpawn(spawner));
        }
    }

    IEnumerator EnemySpawn(EnemySpawner spawner){
        if (firstSpawn)
        {
            yield return new WaitForSeconds(spawner.delayToFirstSpawn);
        }
        foreach (SpawnedEnemy enemy in spawner.enemySpawned)
        {
            float delay = spawner.spawnAtConstantTime ? spawner.timeBetweenSpawn : enemy.delayToNextSpawn;
            Instantiate(enemy.enemyPrefab, spawner.spawnPoint.position, spawner.spawnPoint.rotation, enemiesParent);
            yield return new WaitForSeconds(delay);
        }

        firstSpawn = false;
        if (spawner.spawnInfinitely) {
            spawnerCoroutine = StartCoroutine(EnemySpawn(spawner));
        }
    }
}
