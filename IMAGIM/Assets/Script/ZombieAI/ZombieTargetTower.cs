using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargetTower : ZombieBaseState
{
    Transform tower, player;
    Rigidbody2D rb;

    public override void OnEnter(ZombieController zombie)
    {
        tower = GameObject.FindWithTag("AllyTower").transform;
        rb = zombie.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }
    public override void Update(ZombieController zombie)
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


    public override void OnTriggerEnter2D(ZombieController zombie, Collider2D collider)
    {
        if (collider.tag == "Player") // Or any deployable units
        {
            zombie.enemyInArea += 1;
            zombie.SwitchState(zombie.TargetClosest);
        }
    }

    public override void OnTriggerExit2D(ZombieController zombie, Collider2D collider)
    {
    }
}
