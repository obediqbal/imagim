using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public bar healthbar;
    public int maxHealth;
    public int currentHealth;
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMax(maxHealth);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10;
        }
        healthbar.SetValue(currentHealth);

        if (Input.GetKeyDown(KeyCode.C))
        {
            coins += 1;
        }
    }
}
