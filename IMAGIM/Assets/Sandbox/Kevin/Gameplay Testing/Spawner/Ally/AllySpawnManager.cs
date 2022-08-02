using DKH.InstancePooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnManager : MonoBehaviour
{
    [SerializeField] AllyType ally;
    [SerializeField] Transform alliesParent;
    public Transform spawnPoint;

    InstancePool _warriors;
    private void Awake()
    {
        _warriors = new InstancePool(ally.allyWarrior.transform, alliesParent);
    }
    public void StartSpawning()
    {
        _warriors.InstantiateFromPool(spawnPoint.position, spawnPoint.rotation);
    }
}
