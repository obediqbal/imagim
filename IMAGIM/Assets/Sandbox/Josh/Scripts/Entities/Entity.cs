using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    /// <summary>
    /// Represents a MonoBehaviour damagable entity that has attributes, can spawn and despawn.
    /// </summary>
    public abstract class Entity : Poolable, IDamagable
    {
        [Tooltip("This gameobject will be activated/deactivated on pooling")]
        [SerializeField] private GameObject display;
        [SerializeField] private int spawnTeam;
        public Attributes attributes { get; private set; } = new Attributes();
        public Observable<float> health = new Observable<float>(0f);
        public Observable<int> team = new Observable<int>(0);
        public float timeSpawned { get; private set; }
        public override InstancePool.Type PoolType { get; }

        /// <summary>
        /// Spawns this entity, basically Awake for object pooling
        /// </summary>
        public override void Spawn()
        {
            team.Value = spawnTeam;
            display.SetActive(true);
            health.Value = attributes.GetAttributeValue(Attributes.Type.GenericMaxHealth);
            timeSpawned = Time.time;
        }
        /// <summary>
        /// Spawns this entity only resetting its health
        /// </summary>
        public virtual void Repawn()
        {
            health.Value = attributes.GetAttributeValue(Attributes.Type.GenericMaxHealth);
            timeSpawned = Time.time;
        }
        /// <summary>
        /// Destroys this entity, basically OnDestroy for object pooling
        /// </summary>
        public override void Despawn()
        {
            display.SetActive(false);
        }
        public virtual void DealDamage(float amount, IDamagable target)
        {
            target.damage(amount, this);
        }
        /// <summary>
        /// Called when this entity reaches 0 health through damage
        /// </summary>
        protected virtual void OnDeath()
        {

        }
        /// <summary>
        /// Called when this entity kills another entity
        /// </summary>
        protected virtual void OnKill(Entity target)
        {

        }
        // IDamageable
        public void damage(float amount)
        {
            health.Value -= amount;
            if (health.Value < 0)
            {
                OnDeath();
            }
        }

        public void damage(float amount, Entity source)
        {
            damage(amount);
            if (health.Value < 0)
            {
                OnKill(source);
            }
        }

        public float getHealth()
        {
            return health.Value;
        }

        public void setHealth(float health)
        {
            this.health.Value = health;
        }
    }
}