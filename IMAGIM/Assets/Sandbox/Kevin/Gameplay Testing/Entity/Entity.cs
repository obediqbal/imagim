using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName:"Game Units/Entity")]
public abstract class Entity : MonoBehaviour, IDamagable
{
    public abstract void OnCreate();
    public abstract void IsDead();
    public abstract void Team();
    public abstract void TakeDamage(float damage);
    public abstract void SetMaxHealth(float maxHealth);
    public abstract void SetHealth(float amount);
}
