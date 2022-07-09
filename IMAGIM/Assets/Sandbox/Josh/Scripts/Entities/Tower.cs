using System.Collections;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public class Tower : Entity
    {
        public override InstancePool.Type PoolType => InstancePool.Type.Tower;
        protected override void OnDeath()
        {
            base.OnDeath();
            InstancePool.Despawn(this);
        }
    }
}