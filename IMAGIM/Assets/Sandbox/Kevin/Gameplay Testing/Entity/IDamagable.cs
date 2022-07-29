using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void SetMaxHealth(float maxHealth);
    public void SetHealth(float amount);
    
}
