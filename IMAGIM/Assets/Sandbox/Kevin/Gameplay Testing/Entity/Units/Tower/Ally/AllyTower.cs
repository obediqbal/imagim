using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllyTower : TowerBase
{
    public override void Update()
    {
        base.Update();
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
