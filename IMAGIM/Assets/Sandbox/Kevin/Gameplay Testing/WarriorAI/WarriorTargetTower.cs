using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTargetTower : WarriorBaseState
{
    Transform tower, player;
    Rigidbody2D rb;

    public override void OnEnter(WarriorController warrior)
    {
        tower = GameObject.FindWithTag("EnemyTower").transform;
        rb = warrior.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Enemy").transform;
    }
    public override void Update(WarriorController warrior)
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


    public override void OnTriggerEnter2D(WarriorController warrior, Collider2D collider)
    {
        if (collider.tag == "Enemy") // Or any deployable units
        {
            warrior.enemyInArea += 1;
            warrior.SwitchState(warrior.TargetClosest);
        }
    }

    public override void OnTriggerExit2D(WarriorController warrior, Collider2D collider)
    {
    }
}
