using System.Collections;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public class Player : Entity
    {
        public override InstancePool.Type PoolType => InstancePool.Type.Player;
        protected override void OnDeath()
        {
            base.OnDeath();
            InstancePool.Despawn(this);
        }
    }
}