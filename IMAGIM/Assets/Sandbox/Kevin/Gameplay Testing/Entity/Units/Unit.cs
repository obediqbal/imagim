using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu(menuName:"Game Units/Unit")]
public class Unit : Entity
{
    public Slider slider;
    public float currentHealth;
    public override void OnCreate()
    {
        throw new System.NotImplementedException();
    }

    public override void IsDead()
    {
        throw new System.NotImplementedException();
    }
    public override void Team()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }

    public override void SetMaxHealth(float maxHealth)
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public override void SetHealth(float health)
    {
        currentHealth = health;
        slider.value = health;
    }
}
