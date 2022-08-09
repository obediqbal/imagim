using DKH.InstancePooling;
using DKH.ResourceSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawnManager : MonoBehaviour
{
    [SerializeField] AllyType ally;
    [SerializeField] Transform alliesParent;
    [SerializeField] Resource fota;
    [SerializeField] float deployCost;
    public Transform spawnPoint;
    InstancePool _warriors;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartSpawning();
        }
    }
    private void Awake()
    {
        _warriors = new InstancePool(ally.allyWarrior.transform, alliesParent);
    }
    public void StartSpawning()
    {
        if (fota.GetValue() >= deployCost)
        {
            _warriors.InstantiateFromPool(spawnPoint.position, spawnPoint.rotation);
            fota.SetValue(fota.GetValue() - deployCost);
        }
        else
        {
            Debug.Log("Not enough Fota!");
        }
    }
}
