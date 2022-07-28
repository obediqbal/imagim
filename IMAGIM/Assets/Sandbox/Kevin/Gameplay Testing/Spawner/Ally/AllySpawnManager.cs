using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnManager : MonoBehaviour
{
    [SerializeField] AllyType ally;
    [SerializeField] Transform alliesParent;
    public Transform spawnPoint;

    public void StartSpawning()
    {
        Instantiate(ally.allyWarrior, spawnPoint.position, spawnPoint.rotation, alliesParent);
    }
}
