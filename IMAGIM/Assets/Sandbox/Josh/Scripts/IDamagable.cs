using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    /// <summary>
    /// Represents a damagable entity that has health and can take damage.
    /// </summary>
    public interface IDamagable
    {
        public void damage(float amount);
        public void damage(float amount, Entity source);
        public float getHealth();
        public void setHealth(float health);
    }
}