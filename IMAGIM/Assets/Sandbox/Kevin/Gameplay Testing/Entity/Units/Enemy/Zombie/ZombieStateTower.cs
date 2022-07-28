using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName: "Game Units/Enemy/Zombie/State/State: Tower")]
public class ZombieStateTower : ZombieStateManager
{
    Transform tower, player;
    Rigidbody2D rb;

    public override void OnEnter(ZombieUnit zombie)
    {
        tower = GameObject.FindWithTag("AllyTower").transform;
        rb = zombie.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    public override void Update(ZombieUnit zombie)
    {
        if (tower != null)
        {
            float distanceToTower = tower.position.x - zombie.transform.position.x;
            Vector3 direction = distanceToTower < 0 ? Vector3.left : Vector3.right;
            zombie.xOrientation = direction.x;
            zombie.transform.localScale = zombie.xOrientation < 0 ? new Vector3(-5, 5, 1) : new Vector3(5, 5, 1);

            rb.velocity = direction * zombie.speed;
        }
    }


    public override void OnTriggerEnter2D(ZombieUnit zombie, Collider2D collider)
    {
        if (collider.CompareTag("Player") || (collider.CompareTag("Ally"))) // Or any deployable units
        {
            zombie.enemyInArea += 1;
            if (zombie.lockedEnemy == null)
            {
                zombie.lockedEnemy = collider.transform;
            }
            zombie.enemiesInRange.Add(collider.transform);
            zombie.SwitchState(zombie.TargetClosest);
        }
    }

    public override void OnTriggerExit2D(ZombieUnit zombie, Collider2D collider)
    {
    }

}
