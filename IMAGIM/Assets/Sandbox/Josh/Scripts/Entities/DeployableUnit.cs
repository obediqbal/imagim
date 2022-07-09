using System.Collections;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public class DeployableUnit : Entity
    {
        public override InstancePool.Type PoolType => InstancePool.Type.DeployableUnit;
        protected override void OnDeath()
        {
            base.OnDeath();
            InstancePool.Despawn(this);
        }
    }
}