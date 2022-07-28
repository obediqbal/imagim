using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Allies/Warrior/State/State: Tower")]
public class WarriorStateTower : WarriorStateManager
{
    Transform tower, player;
    Rigidbody2D rb;

    public override void OnEnter(WarriorUnit warrior)
    {
        tower = GameObject.FindWithTag("EnemyTower").transform;
        rb = warrior.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    public override void Update(WarriorUnit warrior)
    {
        if (tower != null)
        {
            float distanceToTower = tower.position.x - warrior.transform.position.x;
            Vector3 direction = distanceToTower < 0 ? Vector3.left : Vector3.right;
            warrior.xOrientation = direction.x;
            warrior.transform.localScale = warrior.xOrientation < 0 ? new Vector3(-5, 5, 1) : new Vector3(5, 5, 1);

            rb.velocity = direction * warrior.speed;
        }
    }


    public override void OnTriggerEnter2D(WarriorUnit warrior, Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) // Or any deployable units
        {
            warrior.enemyInArea += 1;
            if (warrior.lockedEnemy == null)
            {
                warrior.lockedEnemy = collider.transform;
            }
            warrior.enemiesInRange.Add(collider.transform);
            warrior.SwitchState(warrior.TargetClosest);
        }
    }

    public override void OnTriggerExit2D(WarriorUnit warrior, Collider2D collider)
    {
    }

}
