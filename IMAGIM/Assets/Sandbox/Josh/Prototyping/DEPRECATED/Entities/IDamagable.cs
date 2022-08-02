using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.DeprecatedEntitySystem
{
    /// <summary>
    /// Represents a damagable entity that has health and can take damage.
    /// </summary>
    [Obsolete("The class is deprecated.", true)]
    public interface IDamagable
    {
        public void damage(float amount);
        public void damage(float amount, Entity source);
        public float getHealth();
        public void setHealth(float health);
    }
}