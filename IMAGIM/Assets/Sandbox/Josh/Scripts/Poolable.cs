using System.Collections;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public abstract class Poolable : MonoBehaviour
    {
        private void Start()
        {
            if (!InstancePool.Contains(this))
            {
                InstancePool.AddToPool(this);
            }
        }
        public abstract InstancePool.Type PoolType { get; }
        public abstract void Spawn();
        public abstract void Despawn();
    }
}